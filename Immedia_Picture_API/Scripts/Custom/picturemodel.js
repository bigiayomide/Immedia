function ViewModel() {
    var self = this;

    var tokenKey = 'accessToken';

    self.getcurrentlocation =function() {
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
    self.user = ko.observable();
    self.longitude = ko.observable();
    self.latitude = ko.observable();

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

    self.getPicturesLonLat = function () {


        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        self.result('');
        self.errors.removeAll();
        var getdata = { longitide: "28.1122679", latitude: "-26.270759299999998", page: 1 };
        console.log(getdata);
        console.log(self.longitude());
        $.ajax({
            type: 'GET',
            url: '/api/Picture/GetLocationByLonLat',
            data: getdata,
            headers: headers
        }).done(function (data) {
            self.result(data);
            
            console.log(self.result());
        }).fail(showError);
    }
    self.favourite = function(data,event) {
        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        console.log(data);
        $.ajax({
            type: 'POST',
            url: '/api/Picture/SavePictureforUser',
            data: data,
            headers: headers
        }).done(function (data) {

        }).fail(showError);

    }

    self.getPictures = function (place) {

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
   
        var getdata = { place_id: place.PlaceId, PlaceId: place.PlaceId };

        self.result('');
        self.errors.removeAll();

        console.log(getdata);
        $.ajax({
            type: 'POST',
            url: '/api/Picture/GetLocationPicturesById',
            data: place,
            headers: headers
        }).done(function (data) {
            self.result(data);
        }).fail(showError);
    }

}

 var app = new ViewModel();
 app.getcurrentlocation();
 app.getPicturesLonLat();
 ko.applyBindings(app);
