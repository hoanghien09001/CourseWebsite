<template>
  <div class="container mt-5">
    <div class="row">
      <div class="col-9">
        <div class="blog">
          <div class="info d-flex justify-content-between">
            <div class="name">
              <img
                width="40px"
                src="../../assets/images/user.jpg"
                class="d-inline-block me-2"
                alt=""
              />
              <p class="d-inline-block">
                <strong> {{ blog.userName }} </strong>
              </p>
            </div>
            <div class="time">
              <p style="font-size: small">{{ blog.createTime }}</p>
            </div>
          </div>
          <div class="title">
            <h1>
              {{ blog.title }}
            </h1>
          </div>
          <div class="content">
            <vue-markdown :source="content" />
          </div>
          <div class="like-comment mt-5">
            <div class="numberOfLikes"><p>20 lượt thích</p></div>
            <div class="like btn me-2">Like</div>
            <div
              class="comment btn"
              @click="toogleCommentBlock = !toogleCommentBlock"
            >
              Thảo luận
            </div>
          </div>
          <div class="comment-block mt-3" v-show="toogleCommentBlock">
            <CommentCard></CommentCard>
            <CommentCard></CommentCard>
            <CommentCard></CommentCard>
            <CommentCard></CommentCard>
          </div>
        </div>
      </div>

      <!-- sidebar bài viết mới -->
      <div class="col-3">
        <h3>Bài viết hay</h3>
        <div class="list-new-blog mt-3" v-for="(qblog,qblogIndex) in qualityBlog"
        :key="qblogIndex"
        >
          <NewBlogCard :newBlog="qblog"></NewBlogCard>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import BlogServices from "@/services/blog/blog.services";
import CommentCard from "@/components/blog/CommentCard";
import VueMarkdown from "vue-markdown-render";

export default {
  props: ["blog"],
  data() {
    return {
      Id: 0,
      blog: {},
      qualityBlog: [],
      toogleCommentBlock: false,
      content: "",
    };
  },
  components: { CommentCard, "vue-markdown": VueMarkdown },
  methods: {},
  mounted() {
    if (this.$store.state.blog.latestBlog.id == this.$route.query.id) {
      this.blog = this.$store.state.blog.latestBlog;
      this.content = this.blog.content;
    } else {
      BlogServices.getAllBlog(
        this.$route.query.pageNumber,
        this.$route.query.pageSize
      )
        .then((res) => {
          this.blog = res.data.find((blog) => blog.id == this.$route.query.id);
          console.log(this.$route.query);
          this.content = this.blog.content;
        })
        .catch((err) => {
          console.log(err);
        });
    }
  },
  computed: {
    createTime() {
      return (
        this.blog.createTime.substring(0, 10) +
        ", " +
        this.blog.createTime.substring(11, 16)
      );
    },
  },
};
</script>

<style scoped>
.v-enter-from {
  opacity: 0;
}

.v-enter-active,
.v-leave-active {
  transition: opacity 2s ease-out;
}
.v-leave-from {
  opacity: 1;
}

.blog {
  padding: 0 15px;
  border-right: 2px solid rgb(184, 182, 182);
  overflow: hidden;
}

.like-comment {
  display: flex;
  /* justify-content: space-between; */
  padding: 10px 0;
  position: relative;
}

.like-comment div.btn {
  border: 1px solid black;
  border-radius: 0;
  padding: 5px 10px;
  cursor: pointer;
  box-sizing: border-box;
}

.like-comment .like:hover {
  background-color: rgb(164, 253, 250);
  font-weight: 600;
}

.numberOfLikes {
  position: absolute;
  top: -20px;
}
</style>
