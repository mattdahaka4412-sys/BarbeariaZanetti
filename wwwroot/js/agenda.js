const agendaData = window.agendaData ?? {};

function selecionarDia(dia, mes, ano) {
    const chaveDia = String(dia);

    const data = agendaData[chaveDia] ?? {
        status: "free",
        clientes: 0,
        ocupado: 0,
        livres: "Dia livre",
        dia: obterNomeDiaSemana(dia, mes, ano),
        data: formatarData(dia, mes, ano),
        atendimentos: []
    };

    document.querySelectorAll(".calendar-day").forEach(btn => {
        btn.classList.remove("selected");
    });

    const botao = document.querySelector(
        `.calendar-day[data-dia="${dia}"]`
    );

    if (botao) {
        botao.classList.add("selected");
    }

    document.getElementById("selectedDayName").innerText =
        capitalizarTexto(data.dia);

    document.getElementById("selectedDayDate").innerText =
        capitalizarTexto(data.data);

    const statusElement =
        document.getElementById("dayStatus");

    statusElement.innerText =
        getStatusText(data.status);

    statusElement.className =
        `day-status status-${data.status}`;

    document.getElementById("occupationText").innerText =
        `${data.ocupado}% ocupado`;

    document.getElementById("clientsText").innerText =
        `${data.clientes} cliente(s) agendado(s)`;

    document.getElementById("freeTimeText").innerText =
        data.livres;

    const fill =
        document.getElementById("occupationFill");

    fill.style.width =
        `${data.ocupado}%`;

    fill.className =
        `occupation-fill ${data.status}`;

    const lista =
        document.getElementById("appointmentList");

    lista.innerHTML = "";

    if (data.atendimentos.length === 0) {
        lista.innerHTML = `
            <div class="agenda-empty-state">
                <h3>Nenhum agendamento para este dia</h3>

                <p>
                    Clique em <strong>Novo Agendamento</strong>
                    para cadastrar um atendimento.
                </p>
            </div>
        `;

        preencherDataDoModal(dia, mes, ano);

        return;
    }

    const gruposPorBarbeiro = data.atendimentos.reduce(
        (grupos, atendimento) => {
            const nomeBarbeiro = atendimento.barbeiro;

            if (!grupos[nomeBarbeiro]) {
                grupos[nomeBarbeiro] = [];
            }

            grupos[nomeBarbeiro].push(atendimento);

            return grupos;
        },
        {}
    );

    Object.entries(gruposPorBarbeiro).forEach(
        ([nomeBarbeiro, atendimentos]) => {
            lista.innerHTML += `
                <div class="barber-agenda-header">
                    <i class="bi bi-person-circle"></i>

                    <div>
                        <strong>${nomeBarbeiro}</strong>

                        <small>
                            ${atendimentos.length}
                            atendimento(s)
                        </small>
                    </div>
                </div>
            `;

            atendimentos.forEach(item => {
                const botoesAcao = item.statusId === 1
                    ? `
                    <div class="appointment-actions">

                        <button type="button"
                            class="appointment-action action-complete"
                            data-bs-toggle="tooltip"
                            data-bs-placement="top"
                            data-bs-title="Concluir atendimento"
                            onclick="abrirModalConclusao(${item.id})">

                            <i class="bi bi-check-lg"></i>
                        </button>

                        <button type="button"
                            class="appointment-action action-cancel"
                            data-bs-toggle="tooltip"
                            data-bs-placement="top"
                            data-bs-title="Cancelar atendimento"
                            onclick="confirmarCancelamento(${item.id})">

                            <i class="bi bi-x-lg"></i>
                        </button>

                        <button type="button"
                            class="appointment-action action-absence"
                            data-bs-toggle="tooltip"
                            data-bs-placement="top"
                            data-bs-title="Cliente não compareceu"
                            onclick="confirmarNaoComparecimento(${item.id})">

                            <i class="bi bi-person-x"></i>
                        </button>

                    </div>
                `
            : "";

            lista.innerHTML += `
                <div class="appointment-card"
                 data-agendamento-id="${item.id}"
                 data-status-id="${item.statusId}">

                    <div class="appointment-main">

                        <div class="appointment-time">
                            <strong>${item.hora}</strong>
                            <small>${item.horaFim}</small>
                        </div>

                        <div class="appointment-info">
                            <strong>${item.cliente}</strong>
                            <span>${item.servico}</span>
                        </div>

                        <div class="appointment-value">
                            ${item.valor}
                            </div>

                        </div>

                    ${botoesAcao}

                </div>
            `;
        });
        }
    );

    inicializarTooltips();

    preencherDataDoModal(dia, mes, ano);
}

function getStatusText(status) {
    switch (status) {
        case "low":
            return "Tranquilo";

        case "medium":
            return "Movimentado";

        case "high":
            return "Quase cheio";

        case "full":
            return "Lotado";

        default:
            return "Livre";
    }
}

function obterNomeDiaSemana(dia, mes, ano) {
    const data = new Date(
        ano,
        mes - 1,
        dia
    );

    return data.toLocaleDateString(
        "pt-BR",
        {
            weekday: "long"
        }
    );
}

function formatarData(dia, mes, ano) {
    const data = new Date(
        ano,
        mes - 1,
        dia
    );

    return data.toLocaleDateString(
        "pt-BR",
        {
            day: "2-digit",
            month: "long",
            year: "numeric"
        }
    );
}

function capitalizarTexto(texto) {
    if (!texto) {
        return "";
    }

    return texto.charAt(0).toUpperCase()
        + texto.slice(1);
}

function preencherDataDoModal(dia, mes, ano) {
    const campoData =
        document.getElementById("dataAgendamento");

    if (!campoData) {
        return;
    }

    const mesFormatado =
        String(mes).padStart(2, "0");

    const diaFormatado =
        String(dia).padStart(2, "0");

    campoData.value =
        `${ano}-${mesFormatado}-${diaFormatado}`;
}

function selecionarDiaInicial() {
    const configuracao =
        document.getElementById("agendaConfiguracao");

    if (!configuracao) {
        return;
    }

    const anoCalendario =
        Number(configuracao.dataset.ano);

    const mesCalendario =
        Number(configuracao.dataset.mes);

    const hoje = new Date();

    const calendarioEhMesAtual =
        hoje.getFullYear() === anoCalendario
        && hoje.getMonth() + 1 === mesCalendario;

    let diaInicial;

    if (calendarioEhMesAtual) {
        diaInicial = hoje.getDate();
    } else {
        const diasComAgendamento =
            Object.keys(agendaData)
                .map(Number)
                .sort((a, b) => a - b);

        diaInicial =
            diasComAgendamento[0] ?? 1;
    }

    selecionarDia(
        diaInicial,
        mesCalendario,
        anoCalendario
    );
}

function configurarValorDoServico() {
    const servicoSelect =
        document.getElementById("servicoSelect");

    const valorInput =
        document.getElementById("valorCobrado");

    if (!servicoSelect || !valorInput) {
        return;
    }

    servicoSelect.addEventListener(
        "change",
        function () {
            const option =
                this.options[this.selectedIndex];

            const valor =
                option.dataset.valor;

            if (!valor) {
                valorInput.value = "";
                return;
            }

            valorInput.value =
                Number(valor).toLocaleString(
                    "pt-BR",
                    {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }
                );
        }
    );
}

function abrirModalConclusao(agendamentoId) {
    console.log(
        "Concluir agendamento:",
        agendamentoId
    );
}

function confirmarCancelamento(agendamentoId) {
    console.log(
        "Cancelar agendamento:",
        agendamentoId
    );
}

function confirmarNaoComparecimento(agendamentoId) {
    console.log(
        "Não compareceu:",
        agendamentoId
    );
}

function inicializarTooltips() {
    const elementos =
        document.querySelectorAll(
            '[data-bs-toggle="tooltip"]'
        );

    elementos.forEach(elemento => {
        const tooltipExistente =
            bootstrap.Tooltip.getInstance(elemento);

        if (tooltipExistente) {
            tooltipExistente.dispose();
        }

        new bootstrap.Tooltip(elemento);
    });
}

document.addEventListener(
    "DOMContentLoaded",
    () => {
        selecionarDiaInicial();
        configurarValorDoServico();
    }
);