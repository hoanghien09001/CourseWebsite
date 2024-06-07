<template>
  <!-- slideshow -->
  <div class="container-fluid">
    <div class="picture">
      <img src="../assets/images/slider-teachingregister-1.jpg" alt="" />
      <div class="box">
        <h2>Hãy đến giảng dạy với chúng tôi</h2>
        <p>
          Trở thành giảng viên và thay đổi cuộc sống của mọi người, bao gồm cả
          cuộc sống của chính bạn
        </p>
        <button
          type="button"
          class="btn btn-dark"
          data-bs-toggle="modal"
          data-bs-target="#exampleModal"
        >
          <strong> Bắt đầu </strong>
        </button>
      </div>
    </div>

    <!-- Modal -->
    <div
      class="modal fade"
      id="exampleModal"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h6 class="modal-title" id="exampleModalLabel">
              <strong> Trở thành giảng viên Coursery </strong>
            </h6>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body">
            <p>
              Khám phá cộng đồng hỗ trợ bao gồm nhiều giảng viên online. Được
              phép sử dụng ngay tất cả các tài nguyên sáng tạo khóa học.
            </p>
            <form>
              <input type="text" placeholder="Tên đầy đủ" autocomplete="off" />
              <input
                type="text"
                placeholder="Tên đăng nhập"
                autocomplete="off"
              />
              <input type="text" placeholder="Email" autocomplete="off" />
              <input
                type="password"
                placeholder="Mật khẩu"
                autocomplete="off"
              />

              <button class="btn btn-dark mb-3">Đăng ký</button>
              <h6 class="d-block">
                Bằng việc đăng ký, bạn đồng ý với
                <strong>Điều khoản sử dụng</strong> và
                <strong>Chính sách về quyền riêng tư</strong>
              </h6>
            </form>
          </div>
          <div v-if="!isLoggedIn" class="modal-footer">
            <p>
              Bạn đã có tài khoản?
              <router-link :to="{ name: 'LoginPage' }">
                <strong data-bs-dismiss="modal"> Đăng nhập ngay </strong>
              </router-link>
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- reasons -->
  <div class="container-fluid reason">
    <h2><strong> Có quá nhiều lý do để bắt đầu </strong></h2>
    <ul class="mt-5">
      <li v-for="reason in reasons" :key="reason.title">
        <img :src="reason.img" />
        <h5>{{ reason.title }}</h5>
        <p>
          {{ reason.text }}
        </p>
      </li>
    </ul>
  </div>

  <!-- số lượng khách hàng đã tham gia -->
  <div class="numberOfCustomer">
    <ul>
      <li v-for="customer in numberOfCustomer">
        <h1>
          <strong>
            {{ customer.number }}
          </strong>
        </h1>
        <p>{{ customer.title }}</p>
      </li>
    </ul>
  </div>

  <!-- các bước tham gia -->
  <div class="steps mt-5">
    <h2>
      <strong> Cách thức bắt đầu </strong>
    </h2>
    <ul class="nav nav-pills my-3" id="pills-tab" role="tablist">
      <li
        class="nav-item"
        role="presentation"
        v-for="step in steps"
        :key="step.title"
      >
        <button
          :class="step.active"
          class="nav-link"
          :id="step.idTab"
          data-bs-toggle="pill"
          :data-bs-target="'#' + step.idContent"
          type="button"
          role="tab"
        >
          <h4>{{ step.title }}</h4>
        </button>
      </li>
    </ul>

    <div class="tab-content" id="pills-tabContent">
      <div
        :id="step.idContent"
        class="tab-pane fade show"
        :class="step.active"
        role="tabpanel"
        v-for="step in steps"
        :key="step"
      >
        <div class="content">
          <img :src="step.img" style="float: right" alt="" />
          <div class="mt-5">
            <p>
              {{ step.content1 }}
            </p>
            <strong>{{ step.head }}</strong>
            <p>{{ step.content2 }}</p>
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- button mở form đăng ký -->
  <div class="open-register-form container-fluid">
    <h2>Trở thành giảng viên ngay hôm nay</h2>
    <h4>
      Tham gia một trong những thị trường học tập trực tuyến lớn nhất thế giới
    </h4>
    <button
      type="button"
      class="btn btn-dark"
      data-bs-toggle="modal"
      data-bs-target="#exampleModal"
    >
      Bắt đầu
    </button>
  </div>
</template>

<script>
export default {
  data() {
    return {
      reasons: [
        {
          img: "https://s.udemycdn.com/teaching/value-prop-teach-v3.jpg",
          title: "Giảng dạy theo phong cách của bạn",
          text:
            " Xuất bản khóa học mong muốn, theo cách mong muốn và bạn luôn có quyền " +
            "kiểm soát nội dung của riêng mình",
        },
        {
          img: "https://s.udemycdn.com/teaching/value-prop-inspire-v3.jpg",
          title: "Truyền cảm hứng cho học viên",
          text:
            " Dạy những gì bạn biết và giúp học viên khám phá sở thích, tiếp thu kỹ" +
            " năng mới và thăng tiến trong sự nghiệp của họ.",
        },
        {
          img: "https://s.udemycdn.com/teaching/value-prop-get-rewarded-v3.jpg",
          title: "Nhận phần thưởng",
          text:
            "Mở rộng mạng lưới nghề nghiệp, xây dựng kiến thức chuyên môn và kiếm" +
            "thu nhập từ mỗi lượt ghi danh có trả phí.",
        },
      ],
      numberOfCustomer: [
        {
          number: "62 triệu",
          title: "Học viên",
        },
        {
          number: "Hơn 75",
          title: "Ngôn ngữ",
        },
        {
          number: "830 triệu",
          title: "Lượt ghi danh",
        },
        {
          number: "Hơn 180",
          title: "Quốc gia",
        },
        {
          number: "Hơn 15000",
          title: "Khách hàng doanh nghiệp",
        },
      ],
      steps: [
        {
          title: "Lên kế hoạch cho khung chương trình",
          img: "https://s.udemycdn.com/teaching/plan-your-curriculum-v3.jpg",
          idTab: "pills-step1-tab",
          active: "active",
          idContent: "pills-step1",
          content1:
            "Hãy bắt đầu với niềm đam mê và kiến thức của bạn. Sau đó, bạn có thể " +
            "chọn một chủ đề triển vọng với sự trợ giúp của công cụ Thông tin chi tiết về thị trường." +
            "Bạn là người quyết định phương pháp cũng như kiến thức giảng dạy.",
          head: "Cách chúng tôi trợ giúp bạn",
          content2:
            "Chúng tôi cung cấp nhiều tài nguyên về cách tạo khóa học đầu tiên. Ngoài ra, bảng điều khiển" +
            "của giảng viên và trang khung chương trình của chúng tôi sẽ giúp bạn tổ chức khóa học hiệu quả.",
        },
        {
          title: "Quay video",
          img: "https://s.udemycdn.com/teaching/record-your-video-v3.jpg",
          active: "",
          idTab: "pills-step2-tab",
          idContent: "pills-step2",
          content1:
            "Sử dụng các công cụ cơ bản như điện thoại thông minh hoặc camera DSLR." +
            " Thêm một chiếc micrô tốt là bạn đã sẵn sàng bắt đầu." +
            "Nếu không thích xuất hiện trên camera, bạn chỉ cần ghi lại màn hình của mình." +
            " Dù với cách nào thì bạn cũng nên quay video dài từ 2 tiếng trở lên cho khóa học có trả phí.",
          head: "Cách chúng tôi trợ giúp bạn",
          content2:
            "Nhóm Hỗ trợ của chúng tôi luôn sẵn sàng trợ giúp bạn trong suốt quá trình thực hiện và đưa ra phản hồi về video thử nghiệm.",
        },
        {
          title: "Ra mắt khóa học",
          img: "https://s.udemycdn.com/teaching/launch-your-course-v3.jpg",
          active: "",
          idTab: "pills-step3-tab",
          idContent: "pills-step3",
          content1:
            "Thu thập các xếp hạng và đánh giá đầu tiên bằng cách quảng bá khóa học" +
            "trên mạng xã hội và mạng lưới nghề nghiệp của bạn." +
            "Người dùng sẽ dễ dàng khám phá khóa học của bạn trên cổng khóa học của chúng tôi." +
            " Đây là nơi bạn kiếm được doanh thu từ mỗi lượt ghi danh có trả phí.",
          head: "Cách chúng tôi trợ giúp bạn",
          content2:
            "Công cụ coupon tùy chỉnh cho phép bạn đưa ra ưu đãi thu hút học viên ghi danh, " +
            "đồng thời các chương trình quảng cáo toàn cầu của chúng tôi giúp thúc đẩy lưu lượng truy cập vào khóa học.",
        },
      ],
    };
  },
  computed: {
    isLoggedIn() {
      return this.$store.state.auth.status.isLoggedIn;
    },
  },
};
</script>

<style scoped>
/* --------------------------slideshow-------------------------- */
nav a.router-link-exact-active {
  color: black;
}

.picture img {
  max-width: 100%;
}

.box {
  position: absolute;
  top: 250px;
  padding-left: 64px;
  width: 320px;
}

.box h2 {
  font-weight: bolder;
}
.box button {
  width: 100%;
  border-radius: 0;
  padding: 10px 0;
}

form button {
  width: 100%;
  border-radius: 0;
  border: 1px solid black;
}

input {
  padding: 10px;
  display: block;
  width: 100%;
  margin: auto;
  margin-bottom: 10px;
}

a {
  color: black;
  text-decoration: none;
}

a,
form strong:hover {
  cursor: pointer;
  color: blueviolet;
}

/* --------------------------reason-------------------------- */

.reason {
  padding: 64px;
  background-color: aliceblue;
  margin-top: 50px;
  text-align: center;
}

.reason h4 {
  margin: 24px;
  padding-bottom: 10px;
  font-weight: bold;
  color: rgb(106, 111, 115);
}

ul {
  list-style-type: none;
  display: flex;
  justify-content: space-between;
  margin-bottom: 0;
}

.reason ul h5 {
  font-weight: bold;
}

.reason ul li {
  width: 28%;
}

/* --------------------------number of customer-------------------------- */
.numberOfCustomer {
  background-color: #5624d0;
  color: white;
  padding: 60px 0;
}

.numberOfCustomer ul li {
  margin: 0 auto;
  text-align: center;
}

/* --------------------------cách thức tham gia-------------------------- */
.steps {
  text-align: center;
  width: 60%;
  margin: 0 auto;
}

.steps ul {
  border-bottom: solid 1px grey;
}

.nav.nav-pills button.nav-link {
  background-color: white;
  color: grey;
  border-radius: 0;
}

.nav.nav-pills button.nav-link.active {
  color: black;
  border-bottom: solid 3px #5624d0;
}

.content {
  overflow: hidden;
  text-align: start;
  font-size: large;
}
/*-------------------------- button mở form đăng ký  --------------------------*/
.open-register-form {
  background-color: rgb(247, 249, 250);
  text-align: center;
  margin: 0 auto;
  padding: 60px 0 20px 0;
}

.open-register-form h2 {
  font-weight: bold;
}

.open-register-form h4 {
  width: 550px;
  margin: 25px auto;
}

.open-register-form button {
  border-radius: 0;
  width: 320px;
  font-weight: bold;
  padding: 10px;
}
</style>
