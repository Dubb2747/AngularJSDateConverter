angular.module("DateConverter", [])
    //Attach controller named DateConversionController to AngularJS app
    .controller("DateConversionController", ['$scope', '$http', function ($scope, $http) {
        //Listen for submit event
        $scope.submit = function () {
            //Post form data to Web API through $http, send data in Json with JSON.stringify
            $http.post('/api/dateconversion', JSON.stringify($scope.Date)).
            //Handle successful event
            success(function (data, status, headers, config) {
                //Bind returned data from Web API to AngularJS data elements Message and ErrorMessage
                $scope.Message = data.Message;
                $scope.ErrorMessage = data.ErrorMessage;
                //If error message is not null toggle showError to true and show to null
                //Hide default message if there is a response either way
                if ($scope.ErrorMessage != null)
                {
                    $scope.showError = true;
                    $scope.show = null;
                    $scope.hideDefault = true;
                }
                //If error message is null toggle show to true and showError to null
                else
                {
                    $scope.show = true;
                    $scope.showError = null;
                    $scope.hideDefault = true;
                }
            }).
            //Handle error event
            error(function (data, status, headers, config) {
                alert('There was an error');
            });
        };
    }]);