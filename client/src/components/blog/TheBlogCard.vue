<template>
  <div class="blog-card-preview">
    <div class="avatar-author">
      <img width="50px" src="../../assets/images/user.jpg" alt="" />
    </div>
    <div class="content">
      <div class="name-time">
        <p class="me-3" style="color: #5488c7">{{ blog.userName }}</p>
        <p>{{ createTime }}</p>
        <!-- <p v-if="isBlogUpdated">{{ blog.updateTime.toLocaleString() }}</p> -->
      </div>
      <div class="title">
        <h4>{{ blog.title }}</h4>
      </div>
      <div class="text">
        <p>{{ contentPreview }}</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["blog"],
  computed: {
    // isBlogUpdated() {
    //   return (
    //     this.blog.updateTime.getTime() !== new Date("01/01/0001").getTime()
    //   );
    // },
    createTime() {
      return (
        this.blog.createTime.substring(11, 16) +
        ", " +
        this.blog.createTime.substring(0, 10)
      );
    },
    contentPreview() {
      return this.blog.content.substring(0, 250) + "...";
    },
  },
};
</script>

<style scoped>
.blog-card-preview {
  width: 100%;
  border-bottom: 1px solid rgb(231, 226, 226);
  display: flex;
  margin-bottom: 16px;
  cursor: pointer;
}

.avatar-author {
  margin: 0px 20px 0 0;
}

.content .name-time p {
  display: inline-block;
  margin: 0;
  font-size: small;
  color: grey;
}

.content .title:hover {
  color: rgb(113, 80, 191);
}

.content .text {
  color: #757070;
}
</style>
