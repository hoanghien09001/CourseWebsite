<template>
  <div class="ma-16">
    <div class="text-body-1 font-weight-bold">Câu hỏi:</div>
    <v-textarea label="Câu hỏi" v-model="question"></v-textarea>
    <button type="button" @click="CreateMakeQuestion" class="btn btn-secondary btn-large">Lưu</button>
  </div>
</template>
<script>
import { studentApi } from "@/services/studentApi";
export default {
  data() {
    return {
      studentApi: studentApi(),
      question: "",
    };
  },
  async mounted() {
    const subjectDetailId = this.$route.params.subjectDetailId;
    console.log("sbID: " + subjectDetailId);
  },
  methods: {
    async CreateMakeQuestion() {
      const subjectDetailId = this.$route.params.subjectDetailId;
      console.log("sbID: " + subjectDetailId);
      var res = await this.studentApi
        .CreateMakeQuestion(subjectDetailId, this.question)
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
  },
};
</script>