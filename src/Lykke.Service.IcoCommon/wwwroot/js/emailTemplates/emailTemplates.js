angular.module("app")
    .component("emailTemplates", {
        templateUrl: "js/emailTemplates/emailTemplates.html",
        bindings: {
        },
        controller: emailTemplatesController,
        controllerAs: "vm"
    });

function emailTemplatesController($http) {
    var vm = this;

    vm.templates = [];
    vm.currentTemplate = null;

    vm.$onInit = function () {
        initEditor();
        loadTemplates();
    }

    function loadTemplates() {
        $http.get("api/mail/templates").then(function (response) {
            vm.templates = response.data;
            setCurrentTemplate(vm.templates[0]);
        });
    }

    function initEditor() {
        vm.editor = monaco.editor.create(document.getElementById('editor'), {
            language: 'html',
            minimap: {
                enabled: false
            },
            renderIndentGuides: true
        });
    }

    function setCurrentTemplate(value) {
        vm.currentTemplate = value;
        if (vm.currentTemplate) {
            vm.editor.setValue(vm.currentTemplate.body);
        }
    };
};