
function deleterow(tableID) {                   // necessary to delete the last row of our table 
    var table = document.getElementById(tableID);
    var rowCount = table.rows.length;

    table.deleteRow(rowCount - 1);
}
deleterow('showtable');

$("#showtable").on("click", "#viewBtn", function () {                 // reperto detail
    let id = $(this).closest("tr").find("td").eq(0).html();
    $.ajax({
        url: "/User/Detail",
        type: "POST",
        data: { dt: id },
        success: function (data) {
            if (data.length < 50) {
                alert(data);
            }
            else {
                $("#showmodal .modal-dialog").html(data);
                $("#showmodal").modal("show");
            }
        },
        error: function () {
            alert("No Detail Found");
        }
    });
});

$(".btn-del").on("click", function () {             // delete fields 
    if (confirm("Procedere con l'eliminazione?")) {
        let id = $(this).closest("tr").find("td").eq(0).html();
        const getTabella = window.location.search.split('=');
        let tabella = getTabella[1];
        $.ajax({
            url: "/User/Delete",
            type: 'POST',
            data: {
                del: id,
                tab: tabella
            },
            error: function () {
                alert('Errore impossibile eliminare');
            },
            success: function (data) {
                console.log(data);
                    alert("Eliminazione eseguita con successo");
                    location.reload();
                
            }
        })
    }
});

$(".modifica").on("click", function () {                      // ajax call to update a fields
    let id = $(this).closest("tr").find("td").eq(0).html();
    const getTabella = window.location.search.split('=');
    let tabella = getTabella[1];
    $.ajax({
        url: "/Update/GetReperto",
        type: 'GET',
        data:{
        upd: id,
        tab: tabella
    },
        success: function (data) {

        },
        error: function () {
            alert("internal error");
        }
    })

    

});