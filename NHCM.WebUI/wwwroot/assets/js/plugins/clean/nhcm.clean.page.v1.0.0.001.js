var clean = window.clean = window.clean || {};
(function () {
    clean.page = function (opt) {
        this.opt = opt = opt || {};
        this.el = opt.el;
        this.mainform = {};
        this.subforms = [];
        this.name = opt.name || 'default';
        this.components = this.el.find('.page-component');
        this.init(this.el);
    }
    clean.page.prototype = {
        init: function (opt) {
            var self = this;
            self.construct();
        },
        construct: function (opt) {
            var self = this;
            self.components.each(function (index) {
                this.el = $(this);
                this.page = self;
                if (this.el.attr('type') == 'form' && this.el.hasClass('main-form')) {
                    self.mainform = new clean[this.el.attr('type')](this);
                }
                else if (this.el.attr('type') == 'form' && this.el.hasClass('sub-form')) {
                    var sub = new clean[this.el.attr('type')](this);
                    self.subforms.push(sub);
                }
                else if (this.el.attr('type') === 'actionmenu') {
                    var menu = {};
                    menu.el = this;
                    menu.page = self;
                    new clean[this.el.attr('type')](this);
                }

                else if (this.el.attr('type') == 'form-tail') {
                    $('.main-content').append(this.el);
                }
            });
        },
        loadsubscreen: function (formname) {
            var self = this;
            var path = '/' + formname.substring(formname.indexOf("_") + 1).replace('_', '/') + '/Get';
            var data = {};
            if (!$.isEmptyObject(self.mainform.record)) {
                clean.data.get({
                    async: false, url: path, data: clean.data.json.write(data), dataType: 'html',
                    success: function (msg) {
                        var html = msg;
                        $('.dependent-screens').html(html);
                        var subform = {};
                        subform.el = $('#' + formname);
                        subform.parent = self.mainform;
                        subform.page = self;
                        if (subform.el.hasClass('page-component')) {
                            if (subform.el.attr('type') == 'form' && subform.el.hasClass('sub-form')) {
                                var sub = new clean[subform.el.attr('type')](subform);
                                self.subforms.push(sub);
                            }
                        }
                    }
                });
            }
            else {
                clean.widget.error('فورم اصلی خالی میباشد', 'لطفاً برای اینکه صفحه های فرعی را مشاهده نمائید، ریکارد فورم اصلی را مشخص سازید');
            }
        },
        parameter: function (name) {
            name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
            var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
            var results = regex.exec(location.search);
            
            return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
        }
    };
})();