function ViewModel() {
    var self = this;

    var tokenKey = 'accessToken';

    self.result = ko.observable();
    self.user = ko.observable();

    self.longitude = ko.observable();
    self.latitude = ko.observable();
    self.registerPassword2 = ko.observable();
    self.page = ko.observable();


    self.loginEmail = ko.observable();
    self.loginPassword = ko.observable();
    self.errors = ko.observableArray([]);

    function showError(jqXHR) {

        self.result(jqXHR.status + ': ' + jqXHR.statusText);

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
            if (response.error) self.errors.push(response.error);
            if (response.error_description) self.errors.push(response.error_description);
        }
    }

    self.callApi = function () {
        self.result('');
        self.errors.removeAll();

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'GET',
            url: '/api/values',
            headers: headers
        }).done(function (data) {
            self.result(data);
        }).fail(showError);
    }

    self.getPictures= function()  {
        self.result('');
        self.errors.removeAll();

        var data = {
            lon: "29.8587",
            lat: "31.0218",
            page: 1
        };

        $.ajax({
            type: 'GET',
            url: 'api/Picture/GetLocationPictures',
            data: data
        }).done(function (data) {
            self.result(data);
            
            console.log(self.result());
            }).fail(showError);
    }

}

var app = new ViewModel();
app.getPictures();
ko.applyBindings(app);