<template>
  <div class="container-fluid">
    <div>
      <img src="../assets/images/confirmEmail.png" width="80px" alt="" />
    </div>
    <h1>Xác minh Email của bạn</h1>
    <p>
      Việc xác minh email sẽ giúp bạn tiếp cận nhiều tính năng trên Coursery
      hơn. Hãy nhập mã xác nhận vào ô bên dưới để tham gia vào cộng đồng người
      học trên khắp thế giới.
    </p>

    <input type="text" v-model="confirmCode" placeholder="Code" />
    <button class="btn btn-outline-success ms-3" @click="confirmEmail()">
      <strong> Gửi mã </strong>
    </button>
  </div>
</template>

<script>
import authServices from "@/services/auth.services";

export default {
  data() {
    return {
      confirmCode: "",
    };
  },
  methods: {
    confirmEmail() {
      authServices
        .confirmEmail(this.confirmCode)
        .then((res) => {
          this.$toast.success(res.data);
          this.$router.push({ name: "HomePage" });
        })
        .catch((err) => {
          this.$toast.error(err);
        });
    },
  },
};
</script>

<style scoped>
.container-fluid {
  width: 600px;
  margin: 100px auto;
  text-align: center;
}

input {
  padding: 10px;
}

button {
  border-radius: 0;
  height: 48px;
}
</style>
