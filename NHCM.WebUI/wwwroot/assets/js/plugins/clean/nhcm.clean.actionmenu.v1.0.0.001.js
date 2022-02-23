var clean = window.clean = window.clean || {};
(function () {
    clean.actionmenu = function (opt) {
        this.opt = opt = opt || {};
        this.el = opt.el;
        this.page = opt.page;
        this.name = opt.name || 'subscreens';
        this.actions = this.el.find('[action]');
        this.menus = [];
        this.init();
    }
    clean.actionmenu.prototype = {
        init: function () {
            var self = this;
            self.construct();
        },
        construct: function () {
            var self = this;
            var sidebar = $('.page-sidebar');
            var sidetemp = '<div class="col-md-2 pull-right"><div class="sidebar sidebar-secondary sidebar-default" ><div class="sidebar-content"><div class="sidebar-category"><div class="category-content no-padding"></div></div></div></div></div>';
            if (sidebar) {
                $('.main-content').removeClass('col-md-offset-1');
                $('.main-content').parent().prepend(sidetemp);
                $('.category-content').html(sidebar);

                this.actions = self.el.find('[action]');
                this.actions.bind('click', function () {
                    var act = $(this).attr('action');
                    if (self[act]) self[act]($(this));
                    return false;
                });
            }
        },
        subscreen: function (el) {
            var self = this;
            var action = el;
            var formname = action.attr('page');
            this.page.loadsubscreen(formname);
        }
    };
})();