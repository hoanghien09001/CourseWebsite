<template>
  <div class="container-fluid blog">
    <div class="title">{{ newBlog.userName }} - {{ newBlog.title }}</div>
    <div class="content">{{ contentPreview }}</div>
    <div class="author"></div>
  </div>
</template>

<script>
export default {
  props: ["newBlog"],
  computed: {
    contentPreview() {
      return this.newBlog.content.substring(0, 100);
    },
  },
};
</script>

<style scoped>
.blog {
  border-bottom: 1px solid rgb(230, 225, 225);
  margin-bottom: 20px;
}
.title {
  font-size: large;
}

.content {
  color: grey;
}
</style>
