
//
// cambio l'attriuto al form
// eseguo pio la chiamata per eseguire l'update

let id = localStorage.getItem("upd");
let tabella = localStorage.getItem("tab");

function domModification() {                    // here some DOM modification
    let TITLE = document.getElementById('title').innerHTML;
    console.log(TITLE);
    TITLE = TITLE.replace('Inserimento nuovo', 'Modifica');
    console.log(TITLE);
    document.getElementById('title').textContent = TITLE;
}


function loadReperto(id, tabella) {     // function that load the filed to update
    $.ajax({
        url: "/Update/GetReperto",
        type: 'GET',
        data: {
            upd: id,
            tab: tabella
        },
        success: function (data) {
            $('#reperto').html(data);
            domModification();
        },
        error: function () {
            alert("internal error");
        }
    })
}

document.addEventListener('load', loadReperto(id, tabella));


