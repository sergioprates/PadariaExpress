app.directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            modelCtrl.$parsers.push(function (inputValue) {
                // this next if is necessary for when using ng-required on your input. 
                // In such cases, when a letter is typed first, this parser will be called
                // again, and the 2nd time, the value will be undefined
                if (inputValue == undefined) return ''
                var transformedInput = inputValue.replace(/[^0-9]/g, '');
                if (transformedInput != inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }

                return transformedInput;
            });
        }
    };
})

.directive('onValidSubmit', ['$parse', '$timeout', function ($parse, $timeout) {
    return {
        require: '^form',
        restrict: 'A',
        link: function (scope, element, attrs, form) {
            form.$submitted = false;
            var fn = $parse(attrs.onValidSubmit);
            element.on('submit', function (event) {
                scope.$apply(function () {
                    element.addClass('ng-submitted');
                    form.$submitted = true;
                    if (form.$valid) {
                        if (typeof fn === 'function') {
                            fn(scope, { $event: event });
                        }
                    }
                });
            });
        }
    }

}])
  .directive('validated', ['$parse', function ($parse) {
      return {
          restrict: 'AEC',
          require: '^form',
          link: function (scope, element, attrs, form) {
              var inputs = element.find("*");
              for (var i = 0; i < inputs.length; i++) {
                  (function (input) {
                      var attributes = input.attributes;
                      if (attributes.getNamedItem('ng-model') != void 0 && attributes.getNamedItem('name') != void 0) {
                          var field = form[attributes.name.value];
                          if (field != void 0) {
                              scope.$watch(function () {
                                  return form.$submitted + "_" + field.$valid;
                              }, function () {
                                  if (form.$submitted != true) return;
                                  var inp = angular.element(input);
                                  if (inp.hasClass('ng-invalid')) {
                                      element.removeClass('has-success');
                                      element.addClass('has-error');
                                  } else {
                                      element.removeClass('has-error').addClass('has-success');
                                  }
                              });
                          }
                      }
                  })(inputs[i]);
              }
          }
      }
  }])
;
//.directive('formValidateAfter', function () {
//    var directive = {
//        restrict: 'A',
//        require: 'ngModel',
//        link: link
//    };

//    return directive;

//    function link(scope, element, attrs, ctrl) {
//        var validateClass = 'form-validate';
//        ctrl.validate = false;
//        element.bind('focus', function (evt) {
//            if (ctrl.validate && ctrl.$invalid) // if we focus and the field was invalid, keep the validation
//            {
//                element.addClass(validateClass);
//                scope.$apply(function () { ctrl.validate = true; });
//            }
//            else {
//                element.removeClass(validateClass);
//                scope.$apply(function () { ctrl.validate = false; });
//            }

//        }).bind('blur', function (evt) {
//            element.addClass(validateClass);
//            scope.$apply(function () { ctrl.validate = true; });
//        });
//    }

//});