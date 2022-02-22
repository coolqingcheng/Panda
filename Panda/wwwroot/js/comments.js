Vue.component('comment-item', {
    props: {
        info: {
            type: Object,
            default: {}
        }
    },
    template:
        '<div class="comment-item"> ' +
        '<div class="comment-item-header">' +
        '<div>' +
        '<span class="nickName">{{this.info.nickName}}</span><span class="separator">|</span><span class="time">{{this.info.time}}</span>' +
        '</div>' +
        '<div class="right">' +
        '<i class="ri-chrome-line"></i><span>Chrome</span><span class="separator">|</span><i class="ri-windows-fill"></i><span>Windows10</span>' +
        '</div>' +
        '</div>' +
        '<div class="comment-item-body" v-html="this.info.content"></div>' +
        '<div class="comment-item-children">' +
        '<comment-item v-bind:info="item" v-for="item in this.info.children"></comment-item>' +
        '</div>' +
        ' </div>'
})
Vue.component("paging", {
    props: {
        total: Number,
        size: Number,
        curr: {
            type: Number,
            default: 1
        }
    },
    data: function () {
        return {
            index: 1
        }
    },
    created() {
        console.log('create:' + this.size)
    },
    computed: {
        pageCount: function () {
            if (this.total <= this.size) return 0
            if (this.total % this.size == 0) {
                return this.total / this.size
            }
            return (this.total / this.size) + 1
        }
    },
    methods: {
        selectHandler(e) {
            console.log(e.target.value)
            this.$emit('change-index', e.target.value)
        }
    },
    watch: {
        curr: function () {
            this.index = this.curr
        }
    },
    template:
        '<div class="paging">' +
        '{{pageCount}}' +
        '' +
        '<select v-model="this.index" @change="selectHandler($event)">' +
        '<option v-for="(item,index) in pageCount" :key="index" :value="item">{{item}}</option>' +
        '</select>' +
        '<div>' +
        '一共:<span>{{this.total}}</span>' +
        '</div>' +
        '</div>'
})


Vue.component('input-box', {
    props: {
        loading: {
            type: Boolean,
            default: true
        }
    },
    data: function () {
        return {
            showLoading: false
        }
    },
    created() {
        this.showLoading = this.loading;
    },
    methods: {
        submit() {
            console.log('提交评价')
            this.showLoading = true;
            let that = this;
            setTimeout(function () {
                that.showLoading = false
            }, 1000)
        }
    },
    template:
        '<div class="input-mask">' +
        '<div class="input-loading" v-show="showLoading">' +
        '<div>提交中.. {{loading}}</div>' +
        '</div>' +
        '<div class="input-box">' +
        '<div class="input-toolbar"></div>' +
        '<div class="input-body">' +
        '<textarea placeholder="支持输入markdown语法"></textarea>' +
        '<button @click="submit()" class="p-button submit">提交</button>' +
        '</div>' +
        '</div>' +
        '</div>'
})

new Vue({
    el: "#app",
    data: {
        loading: false,
        message: 'vue bind',
        comments: [],
        replyId: 0,
        test: {
            nickName: 'test名称',
            time: new Date().toLocaleDateString(),
            content: '<h1>content</h1>',
            children: [
                {
                    nickName: 'test名称',
                    time: new Date().toLocaleDateString(),
                    content: '<h1>content</h1>'
                }, {
                    nickName: 'test名称',
                    time: new Date().toLocaleDateString(),
                    content: '<h1>content</h1>'
                }
            ]
        }
    },
    template:
        '<div>' +
        ' <input-box  v-bind:loading="this.loading"></input-box>' +
        '     <div class="comments">' +
        '       <comment-item v-bind:info="this.test">' +
        '       </comment-item>' +
        '      <paging v-bind:total="100" v-bind:size="5" v-bind:curr="3"></paging>' +
        '</div>' +
        '</div>'
});