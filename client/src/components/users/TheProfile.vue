<template>
  <div class="header">
    <h3><strong>Hồ sơ cá nhân</strong></h3>
    <p>Chỉnh sửa thông tin về bạn</p>
  </div>
  <hr />
  <div class="content">
    <form class="d-flex flex-column mb-3">
      <h5>Thông tin cơ bản:</h5>
      <div class="form-group d-grid">
        <v-img
          :width="366"
          aspect-ratio="16/9"
          cover
          :src="user.avatar"
        ></v-img>
        <!-- <input type="file" placeholder="Họ tên" @change="handleFileChange"  /> -->
        <v-file-input
          clearable
          label="File input"
          variant="outlined"
          v-model="user.avatar"
        ></v-file-input>
      </div>
      <div class="form-group">
        <label class="kh-form-label" for=""><b>Họ tên</b></label>
        <input
          class="form-input"
          type="text"
          placeholder="Họ tên"
          v-model="user.fullname"
        />
      </div>
      <div class="form-group">
        <label class="kh-form-label" for=""><b>Ngày sinh</b></label>
        <input
          class="form-input"
          type="datetime"
          placeholder="Ngày sinh"
          v-model="user.dateOfBirth"
        />
      </div>
      <div class="form-group">
        <label class="kh-form-label" for=""><b>Số điện thoại</b></label>
        <input
          class="form-input"
          type="text"
          placeholder="Số điện thoại"
          v-model="user.phoneNumber"
        />
      </div>
      <div class="form-group">
        <label class="kh-form-label" for=""><b>Email</b></label>
        <input
          class="form-input"
          type="email"
          placeholder="Email"
          v-model="user.email"
        />
      </div>
      <div class="form-group">
        <label class="kh-form-label" for=""><b>Địa chỉ</b></label>
        <input
          class="form-input"
          type="text"
          placeholder="Địa chỉ (Số nhà), tên đường"
          v-model="user.address"
        />
      </div>
      <!-- <div class="form-group">
        <label class="kh-form-label" for=""><b>Phường</b></label>
        <select class="form-input">
          <option value="">Phường</option>
        </select>
      </div>
      <div class="form-group">
        <label class="kh-form-label" for=""><b>Quận/Huyện</b></label>
        <select class="form-input">
          <option value="">Quận/Huyện</option>
        </select>
      </div>
      <div class="form-group">
        <label class="kh-form-label" for=""><b>Tỉnh/thành phố</b></label>
        <select class="form-input">
          <option value="">Tỉnh/thành phố</option>
        </select>
      </div> -->
      <button
        type="button"
        @click="updateUser"
        class="btn btn-secondary btn-large"
      >
        Lưu
      </button>
    </form>
  </div>
</template>

<script>
import { userApi } from "@/services/userApi";

export default {
  data() {
    return {
      userApi: userApi(),
      user: {},
      selectedFile: null,
    };
  },
  async mounted() {
    const userInfo = JSON.parse(localStorage.getItem("user"));
    console.log(userInfo);

    const res = await this.userApi.GetById(userInfo.Id);
    this.user = res.data.data;

    console.log(this.user);
  },
  methods: {
    async updateUser() {
      const id = JSON.parse(localStorage.getItem("user"));
      const res = await this.userApi
        .updateUser(id.Id, this.user)
        .then((res) => {
          this.$toast.success(res.data.message, {
            duration: 5000,
          });
        })
        .catch((err) => {
          this.$toast.error(err.message);
        });
      console.log(res);
    },
    handleFileChange(event) {
      const file = event.target.files[0];
      if (file) {
        const reader = new FileReader();
        reader.onload = (e) => {
          this.user.avatar = e.target.result;
        };
      }
    },
  },
};
</script>

<style>
.form-input {
  height: 2.8rem;
  border: 1px solid #2d2f31;
  border-radius: 0;
  display: block;
  padding: 0 1.6rem;
  width: 50%;
}
.form-group {
  min-width: 18rem;
  max-width: 60rem;
  margin-left: 1rem;
}
.kh-form-label {
  display: flex;
  align-items: center;
  padding-bottom: 0.3rem;
  min-height: 2.8rem;
  margin: 0;
}
.btn-large {
  margin-left: 1rem;
  margin-top: 2rem;
  width: 5rem;
}
</style>