    $('#AddDetail').toggle(false);  // hide all button in the DOM
    $('#hideDetail').toggle(false);
    $('#submitInsert').toggle(false);

    function getSelected(btn) {
        //  console.log(btn);
        $.ajax({
            url: "/Insert/New",
            type: "GET",
            data: { ins: btn },
            success: function (dt) {
                $('#AddDetail').toggle(true);
                $('#submitInsert').toggle(true);
                $('#main').html(dt);
            },
            error: function () {
                alert("Internal Error ");
            }
        });

        }


    let ADD = document.getElementById('AddDetail');
        ADD.addEventListener('click', () => {
        $.ajax({
            url: "/Insert/New",
            type: "GET",
            data: { ins: 'repertodetail' },
            success: function (dt) {
                $('#hideDetail').toggle(true);
                $('#insertDetail').html(dt).toggle(true);
             
            },
            error: function () {
                alert("Internal Error ");
            }
        });
        })

    let hide = document.getElementById('hideDetail');
        hide.addEventListener('click', () => {
        $('#insertDetail').html("");    // remove all the content from DOM and source code
    $('#hideDetail').toggle(false);
    $('#AddDetail').toggle(true);
        })

$("#submitInsert").click(function (e) {    //  ajax call to insert new fields

    e.preventDefault();  
    $.ajax({
        type: "POST",
        url: $('#form').attr('action'),
        data: $('#form').serialize(),
        success: function (data) {
            console.log(data);     
        },
        error: () => {
            alert("Internal error");
        }
    });

    if ($('#formDetail').children().length > 0) {
       // console.log($('#Identificativo').val());
        $.ajax({
            type: "POST",
            url: $('#formDetail').attr('action'),   
            data: {
              id:  $('#Identificativo').val(),
              dati:  $('#formDetail').serialize()   
            },
            success: function (data) {
                console.log(data);
            },
            error: () => {
                alert("Internal error");
            }
        });
    }

});

