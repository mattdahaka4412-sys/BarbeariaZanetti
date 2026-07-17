const agendaData = {
    5: {
        status: "low",
        clientes: 2,
        ocupado: 25,
        livres: "9h00 livres",
        dia: "Domingo",
        data: "05 de julho de 2026",
        atendimentos: [
            { hora: "09:15", cliente: "João Silva", servico: "Corte", barbeiro: "Administrador", valor: "R$ 40,00" },
            { hora: "14:40", cliente: "Pedro Santos", servico: "Barba", barbeiro: "Administrador", valor: "R$ 30,00" }
        ]
    },

    8: {
        status: "medium",
        clientes: 5,
        ocupado: 55,
        livres: "5h20 livres",
        dia: "Quarta-feira",
        data: "08 de julho de 2026",
        atendimentos: [
            { hora: "09:00", cliente: "Carlos Lima", servico: "Corte", barbeiro: "Administrador", valor: "R$ 40,00" },
            { hora: "10:40", cliente: "Marcos Souza", servico: "Corte + Barba", barbeiro: "Administrador", valor: "R$ 60,00" },
            { hora: "14:15", cliente: "Lucas Oliveira", servico: "Barba", barbeiro: "Administrador", valor: "R$ 30,00" }
        ]
    },

    12: {
        status: "high",
        clientes: 8,
        ocupado: 85,
        livres: "1h40 livre",
        dia: "Domingo",
        data: "12 de julho de 2026",
        atendimentos: [
            { hora: "08:00", cliente: "Rafael", servico: "Corte", barbeiro: "Administrador", valor: "R$ 40,00" },
            { hora: "09:20", cliente: "Bruno", servico: "Corte + Barba", barbeiro: "Administrador", valor: "R$ 60,00" },
            { hora: "11:00", cliente: "Felipe", servico: "Barba", barbeiro: "Administrador", valor: "R$ 30,00" }
        ]
    },

    20: {
        status: "full",
        clientes: 12,
        ocupado: 100,
        livres: "Agenda lotada",
        dia: "Segunda-feira",
        data: "20 de julho de 2026",
        atendimentos: [
            { hora: "08:00", cliente: "Agenda cheia", servico: "Dia sem horários disponíveis", barbeiro: "Administrador", valor: "—" }
        ]
    }
};

function selecionarDia(dia) {

    const data = agendaData[dia] ?? {
        status: "free",
        clientes: 0,
        ocupado: 0,
        livres: "12h livres",
        dia: "Dia selecionado",
        data: `${dia} de julho de 2026`,
        atendimentos: []
    };

    document.querySelectorAll(".calendar-day").forEach(btn => {
        btn.classList.remove("selected");
    });

    const botao = document.querySelector(`[data-dia='${dia}']`);

    if (botao) {
        botao.classList.add("selected");
    }

    document.getElementById("selectedDayName").innerText = data.dia;
    document.getElementById("selectedDayDate").innerText = data.data;
    document.getElementById("dayStatus").innerText = getStatusText(data.status);
    document.getElementById("dayStatus").className = `day-status status-${data.status}`;
    document.getElementById("occupationText").innerText = `${data.ocupado}% ocupado`;
    document.getElementById("clientsText").innerText = `${data.clientes} cliente(s) agendado(s)`;
    document.getElementById("freeTimeText").innerText = data.livres;

    const fill = document.getElementById("occupationFill");
    fill.style.width = `${data.ocupado}%`;
    fill.className = `occupation-fill ${data.status}`;

    const lista = document.getElementById("appointmentList");
    lista.innerHTML = "";

    if (data.atendimentos.length === 0) {

        lista.innerHTML = `
            <div class="agenda-empty-state">
                <h3>Nenhum agendamento para este dia</h3>
                <p>Clique em <strong>Novo Agendamento</strong> para cadastrar um atendimento.</p>
            </div>
        `;

        return;
    }

    data.atendimentos.forEach(item => {

        lista.innerHTML += `
            <div class="appointment-card">

                <div class="appointment-time">
                    ${item.hora}
                </div>

                <div class="appointment-info">
                    <strong>${item.cliente}</strong>
                    <span>${item.servico}</span>
                    <small>Barbeiro: ${item.barbeiro}</small>
                </div>

                <div class="appointment-value">
                    ${item.valor}
                </div>

            </div>
        `;
    });

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

document.addEventListener("DOMContentLoaded", () => {

    selecionarDia(new Date().getDate());

    const servicoSelect = document.getElementById("servicoSelect");
    const valorInput = document.getElementById("valorCobrado");

    if (!servicoSelect || !valorInput)
        return;

    servicoSelect.addEventListener("change", function () {

        const option = this.options[this.selectedIndex];
        const valor = option.dataset.valor;

        if (!valor) {

            valorInput.value = "";
            return;

        }

        valorInput.value = Number(valor).toLocaleString("pt-BR", {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        });

    });

});