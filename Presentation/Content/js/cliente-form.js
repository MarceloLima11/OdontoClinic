function adicionarTelefone() {
    var container = document.getElementById("telefones-container");

    var totalCampos = container.querySelectorAll('input[name="Telefones"]').length;
    if (totalCampos >= 3) {
        alert("Você pode adicionar no máximo 3 telefones.");
        return;
    }

    var input = document.createElement("input");
    input.style = "margin-top:8px";
    input.type = "text";
    input.name = "Telefones"; 
    input.className = "form-control";
    container.appendChild(input);
}

function validarTelefonesAntesDeEnviar() {
    var inputs = document.querySelectorAll('input[name="Telefones"]');
    for (let i = 0; i < inputs.length; i++) {
        let telefone = inputs[i].value.trim();

        if (!/^\d+$/.test(telefone)) {
            alert("Telefone deve conter apenas números.");
            return false;
        }

        if (telefone.length < 10) {
            alert("Telefone deve ter pelo menos 10 dígitos.");
            return false;
        }
    }

    return true;
}