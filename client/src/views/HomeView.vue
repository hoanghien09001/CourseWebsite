<template>
  <div class="container-fluid mb-5">
    <div class="container">
      <!-- slideshow -->
      <div
        id="carouselExampleSlidesOnly"
        class="carousel slide"
        data-bs-ride="carousel"
      >
        <div class="ads-block">
          <h3>
            <strong> Chúng tôi sẽ giúp bạn đạt được mục tiêu </strong>
          </h3>
          <p>
            Học chủ đề do bạn chọn từ trình độ cơ bản đến nâng cao. Các khóa học
            có giá từ <strong> ₫ 299.000 </strong>
            đến hết ngày
            {{ new Date().getDate() + "/" + new Date().getMonth() }}.
          </p>
        </div>
        <div class="carousel-inner">
          <div class="carousel-item active">
            <img
              src="../assets/images/slider-homepage-1.jpg"
              class="d-block w-100"
              alt="..."
            />
          </div>
          <div class="carousel-item">
            <img
              src="../assets/images/slider-homepage-2.jpg"
              class="d-block w-100"
              alt="..."
            />
          </div>
        </div>
      </div>
    </div>

    <!-- divider -->
    <div class="divider">
      <h5>
        Được hơn 15.000 công ty và hàng triệu học viên trên khắp thế giới tin
        dùng
      </h5>
      <ul>
        <li>
          <img src="../assets/images/volkswagen.jpg" />
        </li>
        <li>
          <img src="../assets/images/samsung.jpg" />
        </li>
        <li>
          <img src="../assets/images/cisco.jpg" />
        </li>
        <li>
          <img src="../assets/images/att.jpg" />
        </li>
        <li>
          <img src="../assets/images/procter_gamble.jpg" />
        </li>
        <li>
          <img src="../assets/images/hewlett_packard_enterprise.jpg" />
        </li>
        <li>
          <img src="../assets/images/citi.jpg" />
        </li>
        <li>
          <img src="../assets/images/ericsson.jpg" />
        </li>
      </ul>
    </div>

    <!-- list khóa học -->
    <div class="container my-5 list-course">
      <h3>
        <strong> Tuyển tập khóa học rộng lớn </strong>
      </h3>
      <h5>
        Lựa chọn trong số hơn 80 tỷ khóa học online với nhiều nội dung bổ sung
        mới được xuất bản hàng tháng
      </h5>

      <ul class="nav nav-pills my-3" id="pills-tab" role="tablist">
        <li class="nav-item" role="presentation">
          <button
            class="nav-link active"
            id="pills-home-tab"
            data-bs-toggle="pill"
            data-bs-target="#pills-home"
            type="button"
            role="tab"
          >
            Python
          </button>
        </li>
        <li class="nav-item" role="presentation">
          <button
            class="nav-link"
            id="pills-profile-tab"
            data-bs-toggle="pill"
            data-bs-target="#pills-profile"
            type="button"
            role="tab"
          >
            Javascript
          </button>
        </li>
        <li class="nav-item" role="presentation">
          <button
            class="nav-link"
            id="pills-contact-tab"
            data-bs-toggle="pill"
            data-bs-target="#pills-contact"
            type="button"
            role="tab"
          >
            Java
          </button>
        </li>
      </ul>

      <div class="tab-content" id="pills-tabContent">
        <div class="tab-pane fade show active" id="pills-home" role="tabpanel">
          <div class="detail-list-course">
            <h5><strong> Nâng cao phát triển phần mềm với Python </strong></h5>
            <button class="btn btn-outline-secondary">Khám phá Python</button>
          </div>
        </div>
        <div class="tab-pane fade" id="pills-profile" role="tabpanel">
          <h5>Giúp bạn có cơ hội nghề nghiệp với Javascript</h5>
          <button class="btn btn-outline-secondary">Khám phá Javascript</button>
        </div>
        <div class="tab-pane fade" id="pills-contact" role="tabpanel">
          <h5>Khóa học Java chuyên sâu</h5>
          <button class="btn btn-outline-info">Khám phá Java</button>
        </div>
      </div>
    </div>

    <!-- quảng cáo -->
    <div class="container mt-5">
      <div class="advertise">
        <img
          style="float: right"
          src="https://s.udemycdn.com/home/non-student-cta/UB_Promo_800x800.jpg"
          width="400px"
          alt=""
        />
        <div class="text-side">
          <h3>
            Nâng cao kỹ năng của bạn với các khóa học của chúng tôi ngay tại
            Coursery
          </h3>
          <ul>
            <li>
              <span
                >Truy cập không giới hạn 25000 khóa học hàng đầu của Coursery, ở
                mọi nơi và mọi lúc</span
              >
            </li>
            <li>
              <span>Tuyển tập khóa học quốc tế bằng ngôn ngữ</span>
            </li>
            <li>
              <span>Các chứng chỉ hàng đầu về công nghệ và kinh doanh</span>
            </li>
          </ul>

          <router-link :to="{ name: 'RegisterPage' }">
            <button class="btn btn-dark mb-2">Đăng ký ngay</button>
          </router-link>
          <h6>
            Bạn đã có tài khoản?
            <router-link :to="{ name: 'LoginPage' }">
              <strong> Đăng nhập </strong>
            </router-link>
          </h6>
          <img height="50px" src="../assets/images/logo2.png" alt="" />
        </div>
      </div>
    </div>

    <!-- trở thành giảng viên -->
    <div class="container mt-5">
      <div class="become-teacher">
        <div class="row">
          <div class="col-6">
            <img
              class="d-block"
              src="https://s.udemycdn.com/home/non-student-cta/instructor-1x-v3.jpg"
              width="400px"
              alt=""
            />
          </div>
          <div class="col-6">
            <div class="text-side">
              <h3><strong> Trở thành giảng viên </strong></h3>
              <p>
                Giảng viên trên khắp thế giới giảng dạy hàng triệu học viên trên
                Coursery. Chúng tôi cung cấp công cụ và kỹ năng để dạy những gì
                bạn yêu thích.
              </p>

              <router-link :to="{ name: 'TeachingRegisterPage' }">
                <button class="btn btn-outline-dark mb-2">
                  Bắt đầu dạy học ngay hôm nay
                </button>
              </router-link>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  beforeRouteLeave(to, from, next) {
    window.scrollTo(0, 0);
    next();
  },
  data() {
    return {};
  },
};
</script>

<style scoped>
/* --------------------------slideshow-------------------------- */
.carousel {
  position: relative;
}

.ads-block {
  text-align: left;
  position: absolute;
  width: 400px;
  height: 200px;
  z-index: 1;
  top: 64px;
  left: 72px;
  padding: 24px;
  background-color: white;
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
}

/* --------------------------divider-------------------------- */
.divider {
  padding: 64px;
  background-color: aliceblue;
  margin-top: 50px;
  text-align: center;
}

.divider h5 {
  margin: 24px;
  padding-bottom: 10px;
  font-weight: bold;
  color: rgb(106, 111, 115);
}

.divider ul {
  list-style-type: none;
  display: flex;
  justify-content: space-around;
}

.divider ul li img {
  height: 48px;
}

/* --------------------------list khóa học-------------------------- */
.list-course {
  text-align: start;
}

.nav-pills .nav-link {
  color: black;
}

.nav-pills .nav-link.active {
  background-color: white;
  font-weight: bolder;
  color: black;
}

.tab-pane {
  border: solid 1px rgb(106, 111, 115);
}

.detail-list-course {
  padding: 20px;
}

.detail-list-course button {
  border-radius: 0;
}

/* --------------------------quảng cáo-------------------------- */
.advertise {
  overflow: auto;
  width: 900px;
  margin: 0 auto;
}

.advertise .text-side {
  width: 400px;
  font-size: large;
}

.advertise .text-side button {
  border-radius: 0;
}

.advertise .text-side a {
  text-decoration: none;
  color: black;
}

.advertise .text-side strong:hover {
  cursor: pointer;
  color: blueviolet;
}

.advertise .text-side h3 {
  font-weight: bolder;
}

/* --------------------------trở thành giảng viên-------------------------- */
.become-teacher {
  width: 900px;
  margin: 0 auto;
  display: flex;
  justify-content: space-between;
}

.become-teacher .text-side {
  padding-top: 150px;
  font-size: large;
}

.become-teacher .text-side button {
  border-radius: 0;
}
</style>
