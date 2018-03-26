angular.module("app")
    .component("emailTemplates", {
        templateUrl: "js/emailTemplates/emailTemplates.html",
        bindings: {
        },
        controller: emailTemplatesController,
        controllerAs: "vm"
    });

function emailTemplatesController($scope, $http) {
    var vm = this;
    var templatesUrl = "api/mail/templates";
    var editor;

    vm.templateList = [];
    vm.template;

    vm.selectTemplate = selectTemplate;
    vm.deleteTemplate = deleteTemplate;
    vm.save = save;

    vm.$onInit = function () {
        initEditor();
        loadTemplates();
    }

    function loadTemplates() {
        $http
            .get(templatesUrl)
            .then(function (response) {
                vm.templateList = response.data || [];
                selectTemplate();
            });
    }

    function initEditor() {
        editor = monaco.editor.create(document.getElementById('editor'), {
            language: 'razor',
            minimap: {
                enabled: false
            },
            renderIndentGuides: true
        });
    }

    function selectTemplate(template) {
        if (!template) {
            if (!vm.template) {
                template = vm.templateList[0];
            } else {
                template = vm.templateList.find(function (t) {
                    return t.campaignId == vm.template.campaignId && t.templateId == vm.template.templateId;
                })
            }
        }

        vm.template = template || {};
        editor.setValue(vm.template && vm.template.body ? vm.template.body : "");
    };

    function deleteTemplate() {
        if (!vm.template) {
            return;
        }

        var id = vm.template.campaignId + "/" + vm.template.templateId;

        if (!confirm("Are you sure to delete " + id + "?")) {
            return;
        }

        $http
            .delete(templatesUrl + "/" + id)
            .then(
            function (response) {
                var index = vm.templateList.indexOf(vm.template);
                if (index >= 0) {
                    vm.templateList.splice(index, 1);
                    vm.template = null;
                }
                selectTemplate();
            },
            function (response) {
                alert(response.data.errorMessage);
            });
    }

    function save() {
        if (!vm.template) {
            return;
        }

        if (!vm.template.campaignId) {
            alert("CampaignId must not be null or empty");
            return;
        }

        if (!vm.template.templateId) {
            alert("TemplateId must not be null or empty");
            return;
        }

        var body = editor.getValue();

        if (!body) {
            alert("Body must not be null or empty");
            return;
        }

        if ($scope.templateForm.$pristine && vm.template.body === body) {
            alert("There are no changes to save");
            return;
        }

        vm.template.body = body;

        $http
            .post(templatesUrl, {
                emailTemplate: vm.template,
                username: "IcoCommonServiceUI"
            })
            .then(
                function (response) {
                    var index = vm.templateList.indexOf(vm.template);
                    if (index < 0) {
                        vm.templateList.push(vm.template);
                    }
                    alert("Changes saved!");
                },
                function (response) {
                    alert(response.data.errorMessage);
                });
    };
};