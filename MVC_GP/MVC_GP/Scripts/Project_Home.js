$(function () {

    // 轮播
    $(function () {
        //循环轮播到某个特定的帧
        $(".slide-one").click(function () {
            $("#myCarousel").carousel(0);
        });
        $(".slide-two").click(function () {
            $("#myCarousel").carousel(1);
        });
        //失去焦点非空验证
        $("#txtRegName").blur(function () {
            CheckName();
        })
        $("#txtRegPwd").blur(function () {
            CheckPwd();
        })
        $("#txtRegPwd1").blur(function () {
            CheckPwd1();
        })
        $("user_uname").blur(function () {
            CheckLogName();
        })
        $("user_password").blur(function () {
            CheckLogPwd();
        })
        //非空验证方法
        function CheckName() {
            var user_name = $("#txtRegName").val();
            var regName = /^[0-9a-zA-Z]{6,15}$/;
            if (regName.test(user_name)) {
                $("#txtRegName").css("border-color", "#CCC");
            }
            else {
                $("#txtRegName").css("border-color", "#FF0000");
            }
        }
        function CheckPwd() {
            var user_pwd = $("#txtRegPwd").val();
            var regPwd = /^[0-9a-zA-Z]{6,16}$/;
            if (regPwd.test(user_pwd)) {
                $("#txtRegPwd").css("border-color", "#CCC");
            }
            else {
                $("#txtRegPwd").css("border-color", "#FF0000");
            }
        }
        function CheckPwd1() {
            var user_pwd = $("#txtRegPwd").val();
            var user_pwd1 = $("#txtRegPwd1").val();
            if (user_pwd == user_pwd1) {
                $("#txtRegPwd1").css("border-color", "#CCC");
            }
            else {
                $("#txtRegPwd1").css("border-color", "#FF0000");
            }
        }
        function CheckLogName() {
            var user_name = $("user_uname").val();
            var regName = /^[0-9a-zA-Z]{6,15}$/;
            if (regName.test(user_name)) {
                $("user_uname").css("border-color", "#CCC");
            }
            else {
                $("user_uname").css("border-color", "#FF0000");
            }
        }
        function CheckLogPwd() {
            var user_pwd = $("user_password").val();
            var regPwd = /^[0-9a-zA-Z]{6,16}$/;
            if (regPwd.test(user_pwd)) {
                $("user_password").css("border-color", "#CCC");
            }
            else {
                $("user_password").css("border-color", "#FF0000");
            }
        }
    })
})
