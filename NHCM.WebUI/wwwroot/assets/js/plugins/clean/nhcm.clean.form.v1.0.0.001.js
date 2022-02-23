var clean = window.clean = window.clean || {};
(function () {
    clean.form = function (opt) {
        this.opt = opt = opt || {};
        this.OnInit = window[opt.el.attr('onInit')];
        this.el = opt.el;
        this.path = "";
        this.page = opt.page;
        this.parent = opt.parent || {};
        this.prefix = this.el.attr('prefix') || "ux";
        this.record = {};
        this.uploaders = {};
        this.uploaders.photo = {};
        this.uploaders.document = {};
        this.master = {};
        this.tazkira = {};
        this.actions = this.el.find('.div-form-control [action]');
        this.fields = [];
        this.datepickers = [];
        this.modal = {};
        this.processmodal = {};
        this.grid = {};
        this.grid.template = this.el.find('.form-grid');
        this.grid.table = this.el.attr('id').replace('dv', 'gv');
        this.grid.cols = [];
        this.grid.actions = this.el.find('.div-grid-control').html();
        this.validationrule = {};
        this.orgchart = {};
        this.init(this.el);
    }
    clean.form.prototype = {
        init: function (opt) {
            var self = this;
            self.construct();
            //Registering the clean form actions -- Save -- Search -- New
            this.actions.bind('click', function () {
                var act = $(this).attr('action');
                if (self[act]) self[act]();
                return false;
            });

            if (!clean.isEmpty(self.OnInit))
                clean.invoke(self.OnInit, self);
        },
        getactions: function () {
            var self = this;
            self.actions = this.el.find('.div-form-control [action]');
        },
        getfields: function () {
            var self = this;
            self.fields = self.el.find(':text, :radio, :checkbox, input:hidden, select, textarea').not(":button, :submit");
            var filtered = self.fields.filter(function (item, index, arry) {
                if ($(self.fields[item]).attr('id').startsWith(self.prefix)) return self.fields[item];
            });
            self.fields = filtered;
        },
        loaddatepicker: function () {
            var self = this;
            self.el.find('.Shamsi').each(function () {
                var el = $(this);
                var sib = el.attr('sibling');
                var shamsi = el.MdPersianDateTimePicker({
                    targetTextSelector: '#' + el.attr('id'),
                    targetDateSelector: '#' + sib,
                    dateFormat: 'yyyy-MM-dd',
                    isGregorian: false,
                    enableTimePicker: false
                });
                self.datepickers.push(shamsi);
            });

            self.el.find('.Miladi').each(function () {
                var el = $(this);
                var sib = el.attr('sibling');
                var miladi = el.MdPersianDateTimePicker({
                    targetTextSelector: '#' + sib,
                    targetDateSelector: '#' + el.attr('id'),
                    dateFormat: 'yyyy-MM-dd',
                    isGregorian: true,
                    toPersian: true,
                    enableTimePicker: false
                });
                self.datepickers.push(miladi);
            });
        },
        construct: function () {
            var self = this;
            var path = self.el.attr('id');
            self.path = '/' + path.substring(path.indexOf("_") + 1).replace('_', '/');

            if (self.el.attr('category') == 'modal') {
                var modalid = self.prefix + self.el.attr('id') + '_Modal';
                self.el.find('.modal').attr('id', modalid);
                self.modal = $('#' + modalid);

            }

            if (self.el.attr('attachment')) {
                self.el.find('.actions').append('<button type="button" class="btn btn-primary" action="attach" style="float:left;"><i class="icon-attachment position-right"></i>اسناد و ضمایم </button>');
                self.getactions();
            }

            if (self.el.attr('hasprocess')) {
                self.el.find('.actions').append('<button type="button" class="btn btn-primary" action="process" style="float:left; margin-left: 5px;"><i class="icon-loop position-right"></i>طی مراحل </button>');
                self.getactions();
            }


            if (self.el.hasClass('sub-form')) {
                if (!$.isEmptyObject(self.el.find('#' + self.prefix + self.el.attr('parentcol')))) {
                    self.el.append("<input type='hidden' class='auto-gen-hidden search' id='" + self.prefix + self.el.attr('parentcol') + "' value = '" + self.parent.record.id + "' /> ");
                }
            }
            self.getfields();
            self.el.find('select').select2({
                placeholder: "--",
                allowClear: true
            });
            self.el.find('select').on('change', function (evt) {
                $thisVal = $(this).val();
                if ($thisVal != '') {
                    setTimeout(function () {
                        self.el.valid();
                    }, 100);
                } 
                // if eventtype == حکمی hide other fields
                if ($thisVal == 1114) { 
                    $('.nonhokmi').hide();
                    $('.hokmi').show();
                }
                // if eventtype != حکمی hide other fields
                if ($thisVal == 4 || $thisVal == 8 || $thisVal == 15 || $thisVal == 79) {
                    $('.hokmi').hide();
                    $('.nonhokmi').show();
                }

               

            });

            if ($.isEmptyObject(self.modal)) {
                self.loaddatepicker();
            }
            self.el.find('.national-id').each(function () {
                var opt = {};
                opt.el = $(this).find('.national-id-input');
                opt.parent = $(this).parent();
                opt.form = self;
                self.tazkira = new clean.Tazkira(opt);
            });
            $(self.grid.template.find('thead').find('th')).each(function (index) {
                self.grid.cols.push($(this).attr('colname'));
            });
            $(self.grid.template).find('table').addClass('table table-bordered table-hover').attr('id', self.grid.table);
            self.el.parents('.panel').append(self.grid.template);

            if (self.el.find('.img-uploader').length) {
                var ouput = self.el.find('.img-uploader').siblings('.output').attr('id');
                var croppicContainerModalOptions = {
                    uploadUrl: self.path + '/Upload',
                    cropUrl: self.path + '/Crop',
                    downloadUrl: self.path + '/Download',
                    outputUrlId: ouput,
                    modal: true,
                    imgEyecandy: true,
                    doubleZoomControls: false,
                    rotateControls: false,
                    imgEyecandyOpacity: 0.4,
                    loaderHtml: '<div class="loader bubblingG"><span id="bubblingG_1"></span><span id="bubblingG_2"></span><span id="bubblingG_3"></span></div> ',
                    onBeforeImgUpload: function () { console.log('onBeforeImgUpload') },
                    onAfterImgUpload: function () { console.log('onAfterImgUpload') },
                    onImgDrag: function () { console.log('onImgDrag') },
                    onImgZoom: function () { console.log('onImgZoom') },
                    onBeforeImgCrop: function () { console.log('onBeforeImgCrop') },
                    onAfterImgCrop: function () { console.log('onAfterImgCrop') },
                    onReset: function () { console.log('onReset') },
                    onError: function (errormessage) { console.log('onError:' + errormessage) }
                }
                self.uploaders.photo = new Croppic(self.prefix + 'Photo', croppicContainerModalOptions);
            }
            if (self.el.find('.file-attachment').length) {
                var el = self.el.find('.file-attachment');
                self.initiateFileUpload(el);
            }


            if ($('#' + self.grid.table).attr('organogram') == 'true') {

                var bttntext = $('#' + self.grid.table).attr('action-button');
                self.el.find('.div-form-control').find('.form-group').append('<button type="button" class="btn btn-primary" action="chart" showongrid="true"><i class="icon-tree6 position-right"></i>' + bttntext + '</button>');
                self.getactions();
            }
            self.validationrule = self.validation();
        },
        initiateFileUpload: function (e) {
            var self = this;
            self.uploaders.document = e.fileinput({
                uploadUrl: 'adsfasdfadfaf',
                showCancel: false,
                browseLabel: '',
                browseClass: 'btn btn-primary btn-icon',
                removeLabel: '',
                uploadLabel: 'aaa',
                browseIcon: '<i class="icon-plus22"></i> ',
                removeClass: 'btn btn-remove btn-icon',
                removeIcon: '<i class="icon-cancel-square"></i> ',
                layoutTemplates: {
                    caption: '<div tabindex="-1" class="form-control file-caption {class}">\n' + '<span class="icon-file-plus kv-caption-icon"></span><div class="file-caption-name"></div>\n' + '</div>'
                },
                initialCaption: "فایل انتخاب نگردیده"
            }).on('fileselect', function (event, numFiles, label) {
                setTimeout(function () {
                    self.el.valid();
                }, 100);
                var formData = new FormData(self.el[0]);
                $.ajax({
                    url: self.path + '/Upload',
                    data: formData,
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("XSRF-TOKEN",
                            $('input:hidden[name="__RequestVerificationToken"]').val());
                    },
                    context: document.body,
                    cache: false,
                    contentType: false,
                    processData: false,
                    type: 'POST'
                }).always(function (data) {
                    if (data.status > 0) {
                        if (!self.el.find($('#' + self.prefix + 'path')).length)
                            self.el.append("<input type='hidden' class='auto-gen-hidden' id='" + self.prefix + "path' value='" + data.url + "' />");
                        else
                            self.el.find($('#' + self.prefix + 'path')).val(data.url);
                        self.getfields();
                        clean.widget.success(data.text, data.description);
                        self.el.find('.file-caption-name').removeClass('error-upload');
                    }
                    else {
                        self.el.find('.file-caption-name').addClass('error-upload');
                        clean.widget.error(data.text, data.description);
                    }
                    self.el.find('.file-caption-name').html(data.text + "، " + data.description);
                });
            });
        },
        save: function () {
            var self = this;
            if (self.el.hasClass('sub-form')) {
                if (!self.el.find($('#' + self.prefix + self.el.attr('parentcol'))).length) {
                    self.el.append("<input type='hidden' class='auto-gen-hidden search' id='" + self.prefix + self.el.attr('parentcol') + "' value = '" + self.parent.record.id + "' /> ");
                }
            }

            self.getfields();
            var _tazkira = false;
            if (!$.isEmptyObject(self.tazkira)) {
                _tazkira = self.tazkira.validate();
                if (_tazkira) {
                    $('#uxnid').val(self.tazkira.val());
                }
            }
            else {
                _tazkira = true;
            }

            var _photovalid = false;
            if (self.uploaders.photo.hasOwnProperty('options')) {
                _photovalid = self.uploaders.photo.validate();
                if (!_photovalid) {
                    clean.widget.error('عکس اپلود نگردیده', 'کاربر محترم، لطفاً عکس کارمند را انتخاب نماید. دیتا کارمندان بدون عکس ثبت نمیگردد');
                }
            }
            else {
                _photovalid = true;
            }
            if (self.el.valid() && _photovalid && _tazkira) {
                var path = self.path + '/save';
                var data = {};
                self.fields.each(function () {
                    var fld = $(this);
                    var col = fld.attr('id').substring(self.prefix.length);
                    Object.defineProperty(data, col.toString(), { value: fld.val().toString(), enumerable: true });
                });
                clean.data.post({
                    async: false, url: path, data: clean.data.json.write(data), dataType: 'json',
                    success: function (msg) {
                        if (msg.status > 0) {
                            var list = msg.data.list;
                            clean.widget.success(msg.text, msg.description);
                            self.bindtogrid(list);
                        }
                        else {
                            clean.widget.warn(msg.text, msg.description);
                        }
                    }
                });
            }
        },
        search: function (r) {
            var self = r || this;
            var path = self.path + '/search';

            if (self.el.hasClass('sub-form')) {
                if (!self.el.find($('#' + self.prefix + self.el.attr('parentcol'))).length) {
                    self.el.append("<input type='hidden' class='auto-gen-hidden search' id='" + self.prefix + self.el.attr('parentcol') + "' value = '" + self.parent.record.id + "' /> ");
                }
                self.getfields();
            }
            var data = {};
            if (r) {
                data = self.record;
            }
            else {
                self.el.find('input.search, textarea.search, select.search').each(function () {
                    var fld = $(this), col = fld.attr('id').substring(self.prefix.length);
                    Object.defineProperty(data, col.toString(), { value: fld.val().toString(), enumerable: true });
                });
            }
            clean.data.post({
                async: false, url: path, data: clean.data.json.write(data), dataType: 'json',
                success: function (msg) {
                    var list = msg.data.list;
                    if (r) {
                        self.bindtoform(list[0]);
                    }
                    else {
                        self.bindtogrid(list);
                    }
                }
            });
        },
        new: function (opt) {
            var self = this;
            self.fields.each(function () {
                var fld = $(this);
                if (fld.attr('placeholder', 'درج نگردیده')) fld.attr('placeholder', '');

                if (!fld.attr('default'))
                    fld.val(null).change();
            });
            self.el.find('.auto-gen-hidden').remove();
            if (!$.isEmptyObject(self.uploaders.photo)) {
                self.uploaders.photo.destroy();
                self.uploaders.photo.init();
            }
            if (!$.isEmptyObject(self.uploaders.document)) {
                if (self.el.find('.file-attachment').length) {
                    self.el.find('.file-attachment').attr('required', 'required');
                    self.el[0].reset();
                }
            }
            self.getfields();
        },
        test: function () {
            alert('test');
        },
        configure: function (opt) {
        },
        fetch: function (r) {
            var self = this;
            self.search(r);
        },
        bindtoform: function (d) {
            var self = this;
            self.record = d;
            self.new();
            for (var key in d) {
                var control = $('#' + (self.prefix + key).toLowerCase());
                if (clean.isEmpty(d[key]))
                    control.attr('placeholder', 'درج نگردیده')
                else
                    if (!control.attr('default'))
                        control.val(d[key]).change();
                if (control.hasClass("Miladi")) {
                    control.MdPersianDateTimePicker('setDate', new Date(d[key]));
                }
                if (key == 'id' || key == 'parentid') {
                    if (!self.el.find($('#' + self.prefix + key)).length) {
                        self.el.append("<input type='hidden' class='auto-gen-hidden' id='" + self.prefix + key + "' value='" + d[key] + "' />");
                        self.getfields();
                    }
                }

                if (key == 'nid') {
                    self.tazkira.val(d[key]);
                }

                if (key == 'photoPath' && d[key] != null) {
                    self.uploaders.photo.bind(d[key]);
                }

                if (key == 'path') {
                    if (!self.el.find($('#' + self.prefix + key)).length)
                        self.el.append("<input type='hidden' class='auto-gen-hidden' id='" + self.prefix + "path' value='" + d[key] + "' />");
                    self.el.find('.file-attachment').removeAttr('required');
                }
            }

            if (self.el.attr('subform')) {
                var formname = self.el.attr('subform');
                self.page.loadsubscreen(formname);
            }

            $('.sub-form').each(function () {
                var form = $(this);
                if (form.attr('parent') == self.el.attr('id')) {
                    $.each(self.page.subforms, function (index, sub) {
                        if (sub.el.attr('id') == form.attr('id')) {
                            sub.new();
                            sub.search();
                        }
                    });
                }
            });
        },
        download: function (e) {
            var self = this;
            var file = {};
            file.Name = e;
            var xhr = new XMLHttpRequest();
            xhr.open('POST', self.path + '/Download', true);
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
            xhr.setRequestHeader("Content-Type", 'application/json; charset=utf-8');
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {
                    var blob = new Blob([xhr.response], {
                        type: xhr.getResponseHeader("Content-Type")
                    });
                    var url = window.URL.createObjectURL(blob);
                    window.open(url, url, "directories=0,titlebar=0,toolbar=0,location=0,status=0,menubar=0,scrollbars=no,resizable=no,width=400,height=400");
                }
            }
            xhr.responseType = "arraybuffer";
            xhr.send(JSON.stringify(file));
        },
        bindtogrid: function (d) {
            var self = this;
            var row = "";
            var rowclick = $('#' + self.grid.table).attr('bindonclick') ? 'fetch-record' : '';

            $('#' + self.grid.table).DataTable().clear().draw().destroy();
            $.each(d, function (ind, ob) {
                var column = "";
                for (var i = 0; i < self.grid.cols.length; i++) {
                    var colname = self.grid.cols[i].toLowerCase();
                    for (var key in ob) {
                        if (key.toLowerCase() == colname) {

                            if (colname != 'path' && colname != 'remarks') {

                                var va = ob[key];
                                if (clean.isEmpty(ob[key]))
                                    va = 'درج نگردیده';
                                column = column + "<td col='" + key.toLowerCase() + "' data='" + va + "'>" + va + "</td>";
                            }

                         
                            else if (colname == 'path') {

                                var temp = '<button type="button" downloadpath="$path" class="btn-link download-on-click"><i class="icon-download position-right"></i>دریافت فایل</button>'
                                column = column + "<td col='" + key.toLowerCase() + "'>" + temp.replace('$path', ob[key]) + "</td>";
                            }
                            
                            else if (colname == 'remarks') {
                                if (ob[key] != null) {
                                    var temp = '<a class="link" data-popup="tooltip" data-trigger="hover" data-placement="bottom" title="$Text">نمایش</a>'
                                    column = column + "<td col='" + key.toLowerCase() + "'>" + temp.replace('$Text', ob[key]) + "</td>";
                                }
                                else {
                                    column = column + "<td col='" + key.toLowerCase() + "'></td>";
                                }
                            }
                        }
                    }
                    if (colname == 'action') {
                        var temp = '<td class="text-center"><ul class="icons-list"><li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="icon-menu9"></i></a>$content</li></ul></td>';
                        var actions = "";
                        if (ob.parentId === undefined || ob.parentId === null) {
                            var t = $(self.grid.actions).clone();
                            t.find('[action=neighbour]').parent().remove().end();
                            actions = $(t).get(0).outerHTML;
                        }
                        else
                            actions = self.grid.actions;
                        column = column + temp.replace('$content', actions);
                    }


                     

                    if (colname == 'absentdates') {
                        var temp = '<a class="btn btn-primary btn - sm" data-popup="popover" data-placement="bottom" title="تاریخ های غیرحاضر" data-content="$AbsentDates">لست غیرحاضری ها</a>';
                        var actions = "";
                        //if (ob.parentId === undefined || ob.parentId === null) {
                        //    var t = $(self.grid.actions).clone();
                        //    t.find('[action=neighbour]').parent().remove().end();
                        //    actions = $(t).get(0).outerHTML;
                        //}
                        //else

                        $('[data-popup="popover"]').popover({ html: true });
                        actions = self.grid.actions;
                        column = column + temp.replace('$AbsentDates', actions);
                    }





                }
                if (ob.parentNodeId === undefined || ob.parentNodeId === null) {
                    row = row + "<tr role='row' data-tt-id='" + ob.nodeId + "' class='" + rowclick + "' data='" + ob.id + "'>" + column + "</tr>";
                }
                else
                    row = row + "<tr role='row' data-tt-id='" + ob.nodeId + "' data-tt-parent-id='" + ob.parentNodeId + "' class='" + rowclick + "' data='" + ob.id + "'>" + column + "</tr>";
            });
            $('#' + self.grid.table).find('tbody').empty().html(row);

            var tableheight = !$('#' + self.grid.table).attr('fullheight') ? 150 : null;
            var filter = !$('#' + self.grid.table).attr('filter') ? false : true;

            $('#' + self.grid.table).DataTable({
                "paging": false,
                "showExpander": true,
                "ordering": false,
                "info": false,
                "filter": filter,
                autoWidth: true,
                scrollY: tableheight,
                "oLanguage": {
                    "sSearch": "جستجو",
                    "sLengthMenu": "تعداد در هر صفحه _MENU_",
                    "sEmptyTable": "جدول خالی است",
                    "sInfo": "نمایش صفحه _PAGE_ از _PAGES_ صفحه که مجموعاً شامل _MAX_ ریکارد است"
                },
            });

            if ($('#' + self.grid.table).attr('type') == 'treetable') {
                $('#' + self.grid.table).treetable('destroy'); 
                $('#' + self.grid.table).treetable({ expandable: true });


                $('#' + self.grid.table).treetable('collapseAll');


                $('#' + self.grid.table).parents('.dataTables_scrollBody').css({ overflow: 'visible' });
            }
            $('[data-popup="tooltip"]').tooltip({ template: '<div class="tooltip"><div class="bg-primary"><div class="tooltip-arrow"></div><div class="tooltip-inner"></div></div></div>', trigger: 'click' });
            $('#' + self.grid.table).find('tr').click(function () {
                $(this).siblings().removeClass('row-selected');
                $(this).addClass('row-selected');
            });
            $('#' + self.grid.table).find('.fetch-record').click(function () {
                self.record = {};
                self.record.id = $(this).attr('data');
                self.fetch(self);
            });


            $('#' + self.grid.table).find('.grid-action').click(function () {
                var act = $(this).attr('action');
                if (self[act]) self[act](this);
                return false;
            });


            $('#' + self.grid.table).find('.download-on-click').click(function () {
                self.download($(this).attr('downloadpath'));
            });

            $.each(self.actions, function (i, v) {
                var el = $(v);
                if (el.attr('showongrid'))
                    $('#' + self.grid.table + '_wrapper').find('.dataTables_filter').append(el.css({ 'float': 'left', 'margin-right': '5px' }));
            });

            if (!$('#' + self.grid.table).attr('ignoreinitialformbind'))
                self.bindtoform(d[0]);

        },
        attach: function () {
            var self = this;
            if (!$.isEmptyObject(self.record)) {

                path = "/Document/Document/Get";
                var modalid = self.prefix + self.el.attr('id') + '_Modal';
                if ($.isEmptyObject(self.modal)) {
                    var modal = '<div id="' + modalid + '" class="modal fade"><div class="modal-dialog modal-lg"><div class="modal-content"></div></div></div>';
                    var close = '<button type="button" class="btn btn-link close-bttn" data-dismiss="modal"><i class="icon-close2 position-right"></i>صرف نظر</button>'
                    $('.dependent-screens').append(modal);
                    self.modal = $('#' + modalid);
                    var data = {};

                    clean.data.get({
                        async: false, url: path, data: clean.data.json.write(data), dataType: 'html',
                        success: function (msg) {
                            var html = msg;
                            self.modal.find('.modal-content').html(html);
                            var subform = {};
                            subform.el = $('#' + $(html).find('form').attr('id'));
                            subform.el.attr('parent', self.el.attr('id'));
                            var items = self.path.split('/');
                            subform.el.append("<input type='hidden'  id='" + subform.el.attr('prefix') + "module' value='" + items[1] + "' default='" + items[1] + "' />");
                            subform.el.append("<input type='hidden'  id='" + subform.el.attr('prefix') + "item' value='" + items[2] + "' default='" + items[2] + "' />");
                            subform.el.find('.actions').append(close);
                            subform.parent = self;
                            subform.page = self.page;
                            if (subform.el.hasClass('page-component')) {
                                if (subform.el.attr('type') == 'form' && subform.el.hasClass('sub-form')) {
                                    var sub = new clean[subform.el.attr('type')](subform);
                                    self.page.subforms.push(sub);
                                }
                            }
                        }
                    });
                }
                self.modal.modal();
            }
            else {
                var title = 'فورم $formname خالی میباشد';
                var des = 'برای اینکه اسناد و ضمایم مربوط به فورم $formname را مشاهده نمائید، لطفاً ریکارد این فورم را مشخص سازید';
                var heading = self.el.parents('.panel-body').siblings('.panel-heading').find('h1');
                title = title.replace('$formname', $(heading).text());
                des = des.replace('$formname', $(heading).text());
                clean.widget.error(title, des);
            }
        },
        validation: function () {
            var self = this;
            var validator = $(self.el).validate({
                ignore: 'input[type=hidden], input[type=file] .select2-input',
                errorClass: 'validation-error-label',
                successClass: 'validation-valid-label',
                highlight: function (element, errorClass) {
                    $(element).addClass("hrmis-error");
                    if ($(element).hasClass('file-attachment')) {
                        $(element).parents('.input-group').find('.form-control').addClass('hrmis-error');
                    }
                },
                unhighlight: function (element, errorClass) {
                    setTimeout(function () {
                        $(element).removeClass("hrmis-error");
                        if ($(element).hasClass('file-attachment')) {
                            $(element).parents('.input-group').find('.form-control').removeClass('hrmis-error');
                        }
                    }, 50);
                },
                errorPlacement: function (error, element) {
                    if (element.parents('div').hasClass('input-group') && element.siblings('span').hasClass('input-group-addon')) {
                        error.insertBefore(element.parent());
                    }
                    else if (element.siblings('div').hasClass('select2-container')) {
                        error.insertAfter(element.siblings('label'));
                    }
                    else if (element.hasClass('file-attachment')) {
                        error.insertAfter(element.parents('.file-parent').find('label'));

                    }
                    else {
                        error.insertBefore(element);
                    }
                },
                rules: {
                    email: {
                        email: true
                    },
                    SerialNumber: {
                        digits: true,
                        maxlength: 15
                    },
                    Juld: {
                        maxlength: 12
                    },
                    Page: {
                        digits: true,
                        maxlength: 2
                    },
                    No: {
                        digits: true,
                        maxlength: 1
                    }

                }
            });
            return validator;
        },
        update: function (v) {
            var self = this;
            self.record = {};

            var b = $(v);
            var row = b.parents('tr');

            var temp = {};
            temp.id = row.attr('data');
            temp.parentId = 0;
            self.new();

            if (row.attr('data-tt-parent-id')) {
                temp.parentId = row.attr('data-tt-parent-id');
            }
            self.loadDynamicLists(temp.parentId);

            if (temp.id > 0) {
                self.record.id = temp.id;
            }

            if (b.attr('data-attribute')) {
                var attribute = b.attr('data-attribute');
                var value = 0;
                if (attribute == 'parentid') {
                    value = row.attr('data-tt-parent-id');
                }
                else {
                    value = row.attr('data-tt-id');
                }

                if (!self.el.find($('#' + self.prefix + attribute)).length) {
                    self.el.append("<input type='hidden' id='" + self.prefix + attribute + "' value='" + value + "' />");
                    self.getfields();
                }
                else {
                    $('#' + self.prefix + attribute).val(value);
                }
                self.record[attribute] = value;
            }
            self.modal.modal();
            if (self.datepickers.length == 0)
                self.loaddatepicker();

            if (self.record.id > 0) {
                self.fetch(self);
            }
        },
        neighbour: function (v) {
            var self = this;
            var b = $(v);
            var row = b.parents('tr');
            var parentid;
            if (row.attr('data-tt-parent-id')) {
                self.new();
                parentid = row.attr('data-tt-parent-id');
                if (!self.el.find($('#' + self.prefix + 'parentid')).length) {
                    self.el.append("<input type='hidden' class='auto-gen-hidden' id='" + self.prefix + "parentid' value='" + parentid + "' />");
                    self.getfields();
                }
                else {
                    $('#' + self.prefix + 'parentid').val(parentid);
                }
                self.loadDynamicLists(parentid);
                self.modal.modal();
            }
        },
        chart: function () {
            var self = this;
            var table = $('#' + self.grid.table);
            var modal_title = table.attr('modal-title');
            var modal_description = table.attr('modal-description');
            if (table.attr('node-title') && table.attr('node-sub-title') && table.attr('node-description')) {
                var modalid = self.el.attr('id') + '_chart_Modal';
                var modal = '<div id="' + modalid + '" class="modal fade"><div class="modal-dialog modal-full"><div class="modal-content"><div class="panel panel-flat" style="direction: rtl;"><div class="panel-heading"><h1 class="panel-title">' + modal_title + '</h1></div><div class="panel-body" style="padding-bottom: 5px; "><legend class="text-bold">' + modal_description + '</legend><fieldset class="content-group"><div id="chart-container"></div><hr /></fieldset><div class="row"><div class="col-md-12 action-bttns" style="padding:5px;"><button type="button" class="btn btn-link close-bttn" data-dismiss="modal"><i class="icon-close2 position-right"></i>صرف نظر</button></div></div></div></div></div></div></div>';
                $('.dependent-screens').append(modal);
                $('#' + modalid).modal();
                var nodeTemplate = function (data) {
                    return `
                        <div class="title">${data.title}</div>
                        <div class="content">
                            <div class="subtitle">${data.subtitle}</div>
                            <div class="description">${data.description}</div>
                        </div>
                      `;
                };
                var rows = [];
                var node = {};
                node.title = $('#' + self.grid.table).attr('node-title').split(',');
                node.subtitle = $('#' + self.grid.table).attr('node-sub-title').split(',');
                node.description = $('#' + self.grid.table).attr('node-description').split(',');
                var tbl_html = table.find('tbody');
                $(tbl_html).find('tr').each(function (i, v) {
                    var tr = $(v);
                    var row = {};
                    row.id = tr.attr('data-tt-id');
                    row.parent = tr.attr('data-tt-parent-id');
                    row.title = "";
                    row.subtitle = "";
                    row.description = "";
                    $.each(node.title, function (i, k) {
                        var key = k.toLowerCase();
                        var td = tr.find('[col="' + key + '"]');
                        row.title = row.title + ' ' + $(td).attr('data');
                    });
                    $.each(node.subtitle, function (i, k) {
                        var key = k.toLowerCase();
                        var td = tr.find('[col="' + key + '"]');
                        row.subtitle = row.subtitle + ' ' + $(td).attr('data');
                    });
                    $.each(node.description, function (i, k) {
                        var key = k.toLowerCase();
                        var td = tr.find('[col="' + key + '"]');
                        row.description = row.description + ' ' + $(td).attr('data');
                    });
                    rows.push(row);
                });
                var datascource = {};
                if (rows.length > 0) {
                    $.each(rows, function (i, v) {

                        if (v.parent == 0) {
                            datascource.title = v.title;
                            datascource.subtitle = v.subtitle;
                            datascource.description = v.description;
                            datascource.children = self.getchildren(v, rows);
                        }
                    });
                }

                if ($.isEmptyObject(self.orgchart)) {
                    self.orgchart = $('#chart-container').orgchart({
                        'data': datascource,
                        'nodeContent': 'title',
                        'nodeTemplate': nodeTemplate,
                        'exportButton': true,
                        'exportFilename': 'OrgChart',
                        'pan': true,
                        'zoom': true
                    });
                    $('#chart-container').css('overflow-y', 'auto');
                    $('#chart-container').css('max-height', $(window).height() * 0.7);
                    $('#chart-container').css('height', $(window).height() * 0.7);
                    if ($('#chart-container').find('.oc-export-btn').length && !$('.action-bttns').find('.oc-export-btn').length)
                        $('.action-bttns').append($('.oc-export-btn').addClass('btn btn-primary').prepend('<i class="icon-tree6 position-right"></i>'));


                }
                else {
                    self.orgchart.init({ 'data': datascource });
                    $('#chart-container .oc-export-btn').remove();
                }
            }
            else {
                clean.widget.error('اشتباه', 'لطفاً تمام مشخصات مورد ضرورت چارت را واضح سازید');
            }


        },
        getchildren: function (o, rows) {
            var self = this;
            var result = [];
            $.each(rows, function (i, v) {
                var obj = {};
                if (v.parent == o.id) {
                    obj.title = v.title;
                    obj.subtitle = v.subtitle;
                    obj.description = v.description;
                    obj.children = self.getchildren(v, rows);
                    result.push(obj)
                }
            });
            return result;
        },
        loadDynamicLists: function (v) {
            var self = this;
            self.fields.each(function () {
                var fld = $(this);
                if (fld.attr('data-type')) {
                    var elname = fld.attr('id');
                    var datatype = fld.attr('data-type');
                    var path = self.path + '/' + datatype;
                    var data = {};
                    data.id = v;
                    clean.data.post({
                        async: false, url: path, data: clean.data.json.write(data), dataType: 'json',
                        success: function (msg) {
                            var list = msg.data.list;
                            if (list.length > 0) {
                                $('#' + elname + ' option[value]').remove();
                                for (var i = 0; i < list.length; i++) {
                                    $('#' + elname).append("<option value='" + list[i].id + "'>" + list[i].text + "</option>");
                                }
                            }
                        }
                    });
                }
            });
        },
        child: function (v) {
            var self = this;
            var b = $(v);
            var row = b.parents('tr');
            var parentid = row.attr('data');
            self.new();
            if (!self.el.find($('#' + self.prefix + 'parentid')).length) {
                self.el.append("<input type='hidden' class='auto-gen-hidden' id='" + self.prefix + "parentid' value='" + parentid + "' />");
                self.getfields();
            }
            else {
                $('#' + self.prefix + 'parentid').val(parentid);
            }
            self.loadDynamicLists(parentid);
            self.modal.modal();
        },
        remove: function (v) {
            var self = this;
            var b = $(v);
            var row = b.parents('tr');
            var recordid = row.attr('data');
            var path = self.path + '/remove';
            var data = {};
            data.id = recordid;
            clean.data.post({
                async: false, url: path, data: clean.data.json.write(data), dataType: 'json',
                success: function (msg) {
                    //var list = msg.data.list;
                    if (msg.status > 0) {
                        self.bindtogrid(msg.data.list);
                        clean.widget.success(msg.text, msg.description);
                    }
                    else {

                        clean.widget.error(msg.text, msg.description);
                    }
                }
            });
        },
        process: function () {
            var self = this;
            var sub = {};
            var ScreenID;
            if (!self.el.hasClass('sub-form'))
                ScreenID = self.page.parameter('p');
            else
                ScreenID = self.el.find('#pageid').val();
            if (self.record.id === undefined || self.record.id === null) {
                clean.widget.error("کوشش خلاف اصول", "ریکارد انتخاب شده واجد شرایط به طی مراحل نیست");
            }
            else {
                if (!$.isEmptyObject(self.record)) {
                    path = "/Document/Process?p=" + ScreenID;
                    var modalid = self.prefix + self.el.attr('id') + '_Modal_Process';
                    if ($.isEmptyObject(self.processmodal)) {
                        var modal = '<div id="' + modalid + '" class="modal fade"><div class="modal-dialog modal-lg"><div class="modal-content"></div></div></div>';
                        var close = '<button type="button" class="btn btn-link close-bttn" data-dismiss="modal"><i class="icon-close2 position-right"></i>صرف نظر</button>'
                        $('.dependent-screens').append(modal);
                        self.processmodal = $('#' + modalid);
                        clean.data.get({
                            async: false, url: path, data: clean.data.json.write(), dataType: 'html',
                            success: function (msg) {
                                var html = msg;
                                self.processmodal.find('.modal-content').html(html);
                                var subform = {};
                                subform.el = $('#' + $(html).find('form').attr('id'));
                                subform.el.attr('parent', self.el.attr('id'));
                                var items = self.path.split('/');
                                subform.el.find('.actions').append(close);
                                subform.parent = self;
                                subform.page = self.page;
                                if (subform.el.hasClass('page-component')) {
                                    if (subform.el.attr('type') == 'form' && subform.el.hasClass('sub-form')) {
                                        sub = new clean[subform.el.attr('type')](subform);
                                        self.page.subforms.push(sub);
                                        sub.loadDynamicLists(ScreenID);
                                        sub.search();
                                    }
                                }
                            }
                        });
                    }
                    self.processmodal.modal();
                }
                else {
                    var title = 'فورم $formname خالی میباشد';
                    var des = 'برای اینکه طی مراحل اسناد مربوط به فورم $formname را مشاهده نمائید، لطفاً ریکارد این فورم را مشخص سازید';
                    var heading = self.el.parents('.panel-body').siblings('.panel-heading').find('h1');
                    title = title.replace('$formname', $(heading).text());
                    des = des.replace('$formname', $(heading).text());
                    clean.widget.error(title, des);
                }
            }
        },
        loadInModal: function () {

            var self = this;

        }
    };
}
)();