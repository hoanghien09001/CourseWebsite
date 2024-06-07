<template>
  <div class="container mt-5">
    <div class="row">
      <div class="col-8">
        <!-- create blog -->
        <div class="create-blog-container mb-5" v-if="isLoggedIn">
          <router-link
            :to="{ name: 'CreateBlogPage' }"
            class="d-inline-block create-blog me-2"
            data-bs-toggle="tooltip"
            data-bs-placement="right"
            title="Bạn muốn chia sẻ điều gì?"
          >
            <h4 class="d-inline-block create">
              <button class="btn btn-outline-danger">
                <font-awesome-icon size="xl" :icon="['fas', 'pen-to-square']" />
                <strong> Tạo bài viết mới </strong>
              </button>
              <!-- <button class="btn btn-outline-info ms-3">
                <font-awesome-icon size="xl" :icon="['fas', 'table-cells']" />
                <strong> Quản lý bài viết </strong>
              </button> -->
            </h4>
          </router-link>
        </div>

        <div class="header">
          <strong>
            <p>Danh sách {{ totalItems }} bài viết</p>
          </strong>
        </div>

        <!-- hiển thị danh sách blog -->
        <div>
          <div v-if="isBlogEmpty" class="text-center">
            <h3>Danh sách bài viết trống...</h3>
          </div>
          <div v-for="(blog,blogIndex) in allBlog"
          :key="blogIndex">
            <router-link
              :to="{
                name: 'SinglePost',
                query: {
                  id: blog.id,
                  pageNumber: this.currentPage,
                  pageSize: this.perPage,
                },
              }"
            >
              <TheBlogCard :blog="blog"></TheBlogCard>
            </router-link>
          </div>
          <div class="paginate-bar text-center mt-5">
            <vue-awesome-paginate
              :total-items="totalItems"
              :items-per-page="10"
              :max-pages-shown="10"
              v-model="currentPage"
              :on-click="onClickHandler"
              :disable-breakpoint-buttons="true"
            >
              <template #prev-button>
                <span>
                  <font-awesome-icon :icon="['fas', 'arrow-left']" />
                </span>
              </template>
              <template #next-button>
                <span>
                  <font-awesome-icon :icon="['fas', 'arrow-right']" />
                </span>
              </template>
            </vue-awesome-paginate>
          </div>
        </div>
      </div>

      <!-- sidebar bài viết mới -->
      <div class="col-4">
        <h3>Bài viết hay</h3>
        <div class="list-new-blog mt-3">
          <NewBlogCard :newBlog="blog"></NewBlogCard>
          <NewBlogCard :newBlog="blog"></NewBlogCard>
          <NewBlogCard :newBlog="blog"></NewBlogCard>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import TheBlogCard from "@/components/blog/TheBlogCard.vue";
import NewBlogCard from "@/components/blog/NewBlogCard.vue";
import BlogServices from "@/services/blog/blog.services";

export default {
  data() {
    return {
      blog: {
        avatar: "../../assets/images/user.jpg",
        userName: "ahihi",
        title: "test ",
        createTime: Date.now(),
        updateTime: new Date("2001-02-01"),
        content:
          "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical LContrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source.atin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source.",
      },
      currentPage: 1,
      perPage: 10,
      totalItems: 0,
      allBlog: [],
    };
  },
  computed: {
    isLoggedIn() {
      return this.$store.state.auth.status.loggedIn;
    },
    isBlogEmpty() {
      return this.totalItems ? false : true;
    },
  },
  components: {
    TheBlogCard,
    NewBlogCard,
  },
  methods: {
    onClickHandler() {
      BlogServices.getAllBlog(this.currentPage, this.perPage)
        .then((res) => {
          this.allBlog = res.data;
          console.log(this.currentPage);
          console.log(res);
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
  beforeMount() {
    BlogServices.getAllBlog(this.currentPage, this.perPage)
      .then((res) => {
        this.allBlog = res.data;
        this.totalItems = res.totalItems;
        this.$store.dispatch("blog/getNewBlog", this.allBlog[0]);
      })
      .catch((err) => {
        console.log(err);
      });
  },
  mounted() {
    console.log(this.isLoggedIn);
  },
};
</script>

<style scoped>
font-awesome-icon {
  margin-right: 20px;
}

a.create-blog button:hover {
  color: white;
}

.header {
  border-bottom: 1px solid grey;
  margin-bottom: 50px;
}

.header p {
  display: inline-block;
}

a {
  text-decoration: none;
  color: black;
}

.container {
  margin-bottom: 100px;
}
</style>
