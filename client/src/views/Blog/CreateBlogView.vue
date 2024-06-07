<template>
  <div class="container mt-5">
    <div>
      <h2><strong> Tạo bài viết </strong></h2>
      <input
        type="text"
        v-model="blog.title"
        placeholder="Tiêu đề"
        class="d-block form-control my-4"
      />
    </div>
    <div class="create-content">
      <mavon-editor
        v-model="blog.content"
        language="en"
        placeholder="Bắt đầu chỉnh sửa"
        :toolbars="options"
        style="z-index: 98"
      />
    </div>
    <button class="btn mt-5" @click="publishBlog()">Xuất bản</button>
  </div>
  <vue-markdown :source="blog.content" />
</template>

<script>
import VueMarkdown from "vue-markdown-render";
import blogServices from "@/services/blog/blog.services";

export default {
  beforeRouteLeave() {
    if (this.isContentEdited) {
      const reply = window.confirm(
        "Bài viết của bạn chưa được xuất bản. Bạn vẫn muốn thoát?"
      );

      if (!reply) {
        return false;
      }
    }
    window.scrollTo(0, 0);
  },
  components: { VueMarkdown },
  data() {
    return {
      blog: {
        title: "",
        content: "",
      },
      newBlog: {},
      isContentEdited: false,
      options: {
        bold: true,
        italic: true,
        header: true,
        strikethrough: true,
        quote: true,
        ol: true,
        ul: true,
        link: true,
        imagelink: true,
        // code: true,
        // table: true,
        undo: true,
        redo: true,
        fullscreen: true,
        readmodel: true,
        htmlcode: true,

        help: true,
        preview: true,
      },
    };
  },
  watch: {
    blog: {
      handler() {
        if (this.blog.content || this.blog.title) {
          this.isContentEdited = true;
        } else {
          this.isContentEdited = false;
        }
      },
      deep: true,
    },
  },
  methods: {
    publishBlog() {
      blogServices
        .createBlog(this.blog)
        .then((res) => {
          console.log(res);
          this.newBlog = res.data.data;
          this.isContentEdited = false;
          this.$toast.success(res.data.message);

          // thêm blog mới vào store trước khi quay lại blogpage sẽ reset lại listBlog
          this.$store.dispatch("blog/getNewBlog", this.newBlog);

          this.$router.push({
            name: "SinglePost",
            query: { id: this.newBlog.id },
          });
        })
        .catch((err) => {
          this.$toast.error(err.data.message);
          console.log(err);
        });
    },
  },
  mounted() {
    // console.log(this.$store.state.blog.listBlog[0].id);
  },
};
</script>

<style scoped>
button.btn {
  border-radius: 0;
  color: aliceblue;
  background-color: #a435f0;
}

button:hover {
  background-color: #b95ff6;
}
</style>
