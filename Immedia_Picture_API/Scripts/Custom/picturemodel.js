function ViewModel() {
    var self = this;

    var tokenKey = 'accessToken';

    self.getcurrentlocation = function () {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition);
        } else {
            alert("Geolocation is not supported by this browser.");
        }
    }

    function showPosition(position) {
        self.latitude(position.coords.latitude);
        self.longitude(position.coords.longitude);
    }

    self.result = ko.observable();
    self.UserLocation = ko.observable();
    self.longitude = ko.observable();
    self.latitude = ko.observable();
    self.user = ko.observable();
    //Pagging Properties

    self.page = ko.observable(1);
    self.directions = ko.observable(true);
    self.boundary = ko.observable(true);
    self.text = {
        first: ko.observable('First'),
        last: ko.observable('Last'),
        back: ko.observable('«'),
        forward: ko.observable('»')
    };
    self.page.subscribe(function (page) {
        self.getPicturesLonLat(page);
    });

    //Login Properties
    self.registerEmail = ko.observable();
    self.registerPassword = ko.observable();
    self.registerPassword2 = ko.observable();
    self.loginEmail = ko.observable();
    self.loginPassword = ko.observable();


    self.errors = ko.observableArray([]);

    //Error Function
    function showError(jqXHR) {
        var response = jqXHR.responseJSON;
        if (response) {
            if (response.Message) self.errors.push(response.Message);
            if (response.ModelState) {
                var modelState = response.ModelState;
                for (var prop in modelState) {
                    if (modelState.hasOwnProperty(prop)) {
                        var msgArr = modelState[prop]; // expect array here
                        if (msgArr.length) {
                            for (var i = 0; i < msgArr.length; ++i) self.errors.push(msgArr[i]);
                        }
                    }
                }
            }
            if (response.error) {
                self.errors.push(response.error);
                $("#failure").fadeOut("slow");
                $("#failure_message").text(response.error);
                $("#failure").fadeIn("slow");
            }
            if (response.error_description) self.errors.push(response.error_description);
        }
    }

    //Piture API Functions
    self.getPicturesLonLat = function (page) {

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        self.result('');
        self.errors.removeAll();
        var getdata = { longitide: "28.1122679", latitude: "-26.270759299999998", page: page };
        $.ajax({
            type: 'GET',
            url: '/api/Picture/GetLocationByLonLat',
            data: getdata,
            headers: headers
        }).done(function (data) {
            self.result(data);
        }).fail(showError);
    }

    self.favourite = function (data, event) {
        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;

            $.ajax({
                type: 'POST',
                url: '/api/Picture/SavePictureforUser',
                data: data,
                headers: headers
            }).done(function (data) {
                $("#failure").fadeOut("slow");
                $("#success").fadeOut("slow");
                $("#success_message").text("Successfully Added to Favourites");
                $("#success").fadeIn("slow");
            }).fail(showError);
        }
        else {
            OpenDialog();
        }

    }

    self.getUserLocations = function () {

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token
            $.ajax({
                type: 'GET',
                url: '/api/Picture/GetUserLocations',
                headers: headers
            }).done(function (data) {
                self.UserLocation(data);
                console.log(data);
            }).fail(showError);
        }
        else {
            OpenDialog();
        }
    }

    self.UserPictures = function () {

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;

            self.result('');
            self.errors.removeAll();
            $.ajax({
                type: 'GET',
                url: '/api/Picture/GetUserPictures',
                headers: headers
            }).done(function (data) {
                console.log(data);
                self.result(data);
            }).fail(showError);
        }
        else {
            OpenDialog();
        }
    }

    //Login Signup Function

    self.register = function () {
        self.errors.removeAll();

        var data = {
            Email: self.registerEmail(),
            Password: self.registerPassword(),
            ConfirmPassword: self.registerPassword2()
        };

        $.ajax({
            type: 'POST',
            url: '/api/Account/Register',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            $("#success").fadeOut("slow");
            $("#success_message").text("Successfully Registered");
            $("#success").fadeIn("slow");
        }).fail(showError);
    }

    self.login = function () {
        self.errors.removeAll();

        var loginData = {
            grant_type: 'password',
            username: self.loginEmail(),
            password: self.loginPassword()
        };

        $.ajax({
            type: 'POST',
            url: '/Token',
            data: loginData
        }).done(function (data) {
            self.user(data.userName);
            // Cache the access token in session storage.
            $("#failure").fadeOut("slow");
            $("#success").fadeOut("slow");
            $("#success_message").text("Successfully logged in");
            $("#success").fadeIn("slow");
            self.getUserLocations();
            sessionStorage.setItem(tokenKey, data.access_token);
        }).fail(showError);
    }

    self.logout = function () {
        // Log out from the cookie based logon.
        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'POST',
            url: '/api/Account/Logout',
            headers: headers
        }).done(function (data) {
            // Successfully logged out. Delete the token.
            self.user('');
            $("#failure").fadeOut("slow");
            $("#success").fadeOut(3000);
            $("#success_message").html("Successfully logged out");
            $("#success").fadeIn(3000);
            sessionStorage.removeItem(tokenKey);
        }).fail(showError);
    }

    self.getPictures = function (place) {

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }


        self.result('');
        self.errors.removeAll();

        $.ajax({
            type: 'POST',
            url: '/api/Picture/GetLocationPicturesById',
            data: place,
            headers: headers
        }).done(function (data) {
            self.result(data);
        }).fail(showError);
    }
    function OpenDialog() {
        var $form_modal = $('.user-modal'),
            $form_login = $form_modal.find('#login'),
            $form_signup = $form_modal.find('#signup'),
            $form_forgot_password = $form_modal.find('#reset-password'),
            $form_modal_tab = $('.switcher'),
            $tab_login = $form_modal_tab.children('li').eq(0).children('a'),
            $tab_signup = $form_modal_tab.children('li').eq(1).children('a'),
            $forgot_password_link = $form_login.find('.form-bottom-message a'),
            $back_to_login_link = $form_forgot_password.find('.form-bottom-message a'),
            $main_nav = $('.signup-nav');
        // on mobile open the submenu
        $main_nav.children('ul').toggleClass('is-visible');

        // on mobile close submenu
        $main_nav.children('ul').removeClass('is-visible');
        //show modal layer
        $form_modal.addClass('is-visible');
        //show the selected form
        $form_login.addClass('is-selected');
        $form_signup.removeClass('is-selected');
        $form_forgot_password.removeClass('is-selected');
        $tab_login.addClass('selected');
        $tab_signup.removeClass('selected');
    }
}


 var app = new ViewModel();
 app.getcurrentlocation();
 app.getPicturesLonLat(app.page());
ko.applyBindings(app);
