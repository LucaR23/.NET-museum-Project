
    $('#AddDetail').toggle(false);
    $('#hideDetail').toggle(false);
    $('#submitInsert').toggle(false);

    function getSelected(btn) {
        //  console.log(btn);
        $.ajax({
            url: "/User/Insert",
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
            url: "/User/Insert",
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
        $('#insertDetail').toggle(false);
    $('#hideDetail').toggle(false);
    $('#AddDetail').toggle(true);
        })
