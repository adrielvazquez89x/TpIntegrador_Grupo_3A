function alterDanger(box, span, message) {

    span.textContent = message;
    span.classList.add("text-danger");

    box.classList.remove("is-valid");
    box.classList.add("is-invalid");
}


function alterSuccess(box, span, message) {

    span.textContent = message;
    span.classList.add("text-success");

    box.classList.remove("is-invalid");
    box.classList.add("is-valid");
}

function validateNewStock(color, talle, cantidad, spanColor, spanTalle, spanCant) {
    console.log(color.value);
    console.log(talle.value);
    console.log(cantidad.value);

    console.log(spanColor.textContent);

    let arr = [color, talle, cantidad, spanColor, spanTalle, spanCant];

    //if (color.value = "0") {
    //    console.log("La puta madre que paso");
    //    alterDanger(color, spanColor, "Seleccione un color");
    //}

    for (let x = 0; x < arr.length / 2; x++) {
        if (arr[x].value === "0" || arr[x].value === "") {
            alterDanger(arr[x], arr[x + 3], "Complete este campo");
            return false;
        }
        else {
            alterSuccess(arr[x], arr[x + 3], "");
        }
    }
}

