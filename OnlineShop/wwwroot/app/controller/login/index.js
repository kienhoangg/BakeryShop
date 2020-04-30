var loginController = function () {
    this.initialize = function () {
        registerEvents();
    }

    var registerEvents = function () {
        $('#frmLogin').validate({
            errorClass: 'red',
            ignore: [],
            rules: {
                userName: {
                    required: true
                },
                password: {
                    required: true
                }
            }
        })
        $('#btnLogin').on('click', function (e) {
            if ($('#frmLogin').valid) {
                e.preventDefault();
                var user = $('#txtUserName').val();
                var password = $('#txtPassword').val();
                var returnUrl = $('#ReturnUrl').val();
                login(user, password, returnUrl);
            }      
        })
    }

    var login = function (userName, password, returnUrl) {
        var newData = {
            UserName: userName,
            Password: password
        }
        $.ajax({
            type: 'POST',
            data: {
                returnUrl: returnUrl,
                UserName: userName,
                Password: password
            },
            dataType: 'JSON',
            url: '/Admin/Login/Authen',
           
            success: function (res) {
                if (res.Success === true) {
                    if (res.Message != null) {
                        window.location.href = res.Message;
                    }
                    else {
                        window.location.href = "/Admin/Home";
                    }
                  
                }
                else {
                    common.notify('Login Failed !', 'error');
                }
            },
        })
    }
    
}