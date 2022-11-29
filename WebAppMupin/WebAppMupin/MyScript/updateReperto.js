let id = localStorage.getItem("upd");
let tabella = localStorage.getItem("tab");

function domModification() {                    // here some DOM modification
    let TITLE = document.getElementById('title').innerHTML;  
    TITLE = TITLE.replace('Inserimento nuovo', 'Modifica');  // change title
    document.getElementById('title').textContent = TITLE;

    let tipoReperto = TITLE.split(' ').pop();  // change form action attribute
    let action = '/Update/Update' + tipoReperto;

    $('#form').attr('action',action);
  
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
            loadDetail(id);
        },
        error: function () {
            alert("internal error");
            console.log("impossible to load the field")
        }
    })
}


document.addEventListener('load', loadReperto(id, tabella));

function loadDetail(idRep) {
    $.ajax({
        url: "/Update/GetRepertoDetail",
        type: 'GET',
        data: {
        upd: idRep,
        tab: 'repertodetail'
        },
        success: function (form) {
            $('#detail').html(form);

            let action = '/Update/UpdateDetail';
            $('#formDetail').attr('action', action);

        },
        error: function () {
            alert("Internal error");
        }
    })

}

$('#submit').on("click", function () {

    $.ajax({
        url: $('#form').attr('action'),
        type: 'POST',
        data: $('#form').serialize(),
        success: function (data) {
            alert(data);
        },
        error: function () {
            alert("Internal Error");
        }

    })
});
