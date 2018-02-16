﻿angular.module("app")
    .component("emailTemplates", {
        templateUrl: "js/emailTemplates/emailTemplates.html",
        bindings: {
        },
        controller: emailTemplatesController,
        controllerAs: "vm"
    });

function emailTemplatesController($http) {
    var vm = this;
    var templatesUrl = "api/mail/templates";
    var editor;

    vm.templateList = [];
    vm.template;

    vm.selectTemplate = selectTemplate;
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

        vm.template = template || { _isNew: true };
        editor.setValue(vm.template && vm.template.body ? vm.template.body : "");
    };

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

        vm.template.body = editor.getValue();

        if (!vm.template.body) {
            alert("Body must not be null or empty");
            return;
        }

        $http
            .post(templatesUrl, vm.template)
            .then(
                function (response) {
                    if (vm.template._isNew) {
                        vm.templateList.push(vm.template);
                        delete vm.template._isNew;
                    }
                    alert("Changes saved!");
                },
                function (response) {
                    alert(response.data.errorMessage);
                });
    }
};