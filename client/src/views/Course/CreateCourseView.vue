<template>
  <div class="container mt-5">
    <div class="start-create d-flex justify-content-between">
      <p>Bắt đầu tạo khóa học</p>
      <button @click="turnOnCreate = !turnOnCreate">
        {{ turnOnCreate ? "Hủy bỏ" : "Tạo khóa học của bạn" }}
      </button>
    </div>

    <transition-group name="slide">
      <div class="ads" v-if="!turnOnCreate">
        <div class="row">
          <div class="col-12 box1">
            <div class="row">
              <div class="image col-4">
                <img
                  src="https://s.udemycdn.com/instructor/dashboard/engaging-course.jpg"
                  alt=""
                />
              </div>
              <div class="content col-8">
                <h4>Tạo khóa học thu hút</h4>
                <p>
                  Dù đã giảng dạy nhiều năm hay mới dạy lần đầu, bạn vẫn có thể
                  tạo nên một khóa học hấp dẫn. Chúng tôi đã biên soạn các tài
                  nguyên và phương pháp hay nhất để giúp bạn tiến bộ, bất kể
                  vạch xuất phát của bạn ở đâu
                </p>
                <h4>Bắt đầu tạo video</h4>
                <p>
                  Bài giảng video chất lượng có thể giúp khóa học của bạn trở
                  nên khác biệt. Hãy sử dụng tài nguyên của chúng tôi để tìm
                  hiểu thông tin cơ bản.
                </p>
                <h4>Xây dựng đội ngũ học viên của bạn</h4>
                <p>
                  Mang lại thành công cho khóa học của bạn bằng cách xây dựng
                  đội ngũ học viên.
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div v-else class="create">
        <CreateCourse></CreateCourse>
      </div>
    </transition-group>
  </div>
</template>

<script>
import CreateCourse from "@/components/course/CreateCourse.vue";

export default {
  components: {
    CreateCourse,
  },
  data() {
    return {
      turnOnCreate: false,
    };
  },
};
</script>

<style scoped>
.start-create p {
  margin-top: 15px;
}

.start-create {
  padding: 48px;
  border: 1px solid rgb(209, 215, 220);
  margin: 48px 0 60px 0;
  /* box-shadow: 0 4px 6px 0 rgba(0, 0, 0, 0.2), 0 5px 5px 0 rgba(0, 0, 0, 0.19); */
}

button {
  padding: 0 48px;
  background-color: rgb(164, 53, 240);
  color: white;
  font-weight: bold;
}

button:hover {
  background-color: rgb(124, 10, 200);
}

.box1 {
  display: flex;
  justify-content: center;
  padding: 0;
}

.box1 .image img {
  display: inline-block;
  margin: 0 auto;
}
.box1 .content {
  margin: 50px 50px;
  width: 500px;
}

.box1 .content p {
  border-left: 2px solid rgb(164, 53, 240);
  padding: 10px 20px;
}

.box1 .content h4 {
  display: inline-block;
  /* padding: 0 20px; */
  font-weight: bold;
}

.box1 .content p:hover {
  box-shadow: 0 4px 6px 0 rgba(0, 0, 0, 0.2), 0 5px 5px 0 rgba(0, 0, 0, 0.19);
  border-radius: 5px;
}

/* ============transition================== */
.slide-enter-active {
  transition: all 0.3s ease-out;
}

.slide-leave-active {
  transition: all 0.2s cubic-bezier(1, 0.5, 0.8, 1);
}

.slide-enter-from,
.slide-leave-to {
  transform: translateX(20px);
  opacity: 0;
}
</style>
