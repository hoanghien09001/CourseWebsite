<template>
    <div class="ma-16">                                                                   <!--Câu hỏi-->
        <div>
            <span class="text-h6 font-weight-bold">Câu hỏi: </span>
        </div>
        <div class="d-flex"
        >
          <div class="ma-2">                                                <!--avatar-->
            <v-avatar color="surface-variant" size="30"
                ><img :src="question.avatar" style="width: inherit;" alt=""
              /></v-avatar>
          </div>
          <div class="w-75">                                                             <!--content-->
            <div class="text-body-1 font-weight-bold ma-2">{{question.userName}}</div>
            <div class="text-body-2 ma-2">{{ question.question }}</div>
            <div class="d-flex">
              <div class="text-caption ma-2">{{question.subjectDetailName}}</div>
              <div class="text-caption ma-2">{{question.createTime}}</div>
            </div>
          </div>
          
        </div>
    </div>
    <div class="ma-16">                                                                   <!--Câu hỏi-->
        <div>
            <span class="text-subtitle-1 font-weight-bold">{{ question.numberOfAnswer }} câu trả lời </span>
        </div>
        <div class="d-flex"
        v-for="(answer,answerIndex) in lstAnswer"
        :key="answerIndex"
        >
          <div class="ma-2">                                                <!--avatar-->
            <v-avatar color="surface-variant" size="30"
                ><img :src="answer.avatar" style="width: inherit;" alt=""
              /></v-avatar>
          </div>
          <div class="w-75">                                                             <!--content-->
            <div class="text-body-1 font-weight-bold ma-2">{{ answer.userName }}</div>
            <div class="text-body-2 ma-2">{{answer.answer}}</div>
            <div class="d-flex">
              <div class="text-caption ma-2">{{answer.createTime}}</div>
            </div>
          </div>
          
        </div>
    </div>
</template>
<script>
import {studentApi} from '@/services/studentApi'
export default {
    data() {
        return {
            studentApi: studentApi(),
            lstAnswer:[],
            question:{}
        }
    },
    async mounted() {
        const questionId = this.$route.params.questionId;
        console.log("questionID: " + questionId);
        var res = await this.studentApi.GetAllAnswer(questionId);
        this.lstAnswer = res.data.data;
        var res2 = await this.studentApi.GetQuestionById(questionId);
        this.question = res2.data.data;
        console.log(this.question);
    },
}
</script>