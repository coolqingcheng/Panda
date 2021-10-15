<template>
  <div class="q-login">
    <div class="q-login-box" v-loading="loading">
      <div class="q-login-header">
        <h1>Element-Plus-Admin</h1>
      </div>
      <el-form :model="loginForm" label-width="70px" :rules="loginRules" ref="form">
        <el-form-item label="用户名" prop="userName">
          <el-input placeholder="输入用户名" v-model="loginForm.userName"></el-input>
        </el-form-item>
        <el-form-item label="密码" prop="pass">
          <el-input placeholder="输入密码" v-model="loginForm.pass"></el-input>
        </el-form-item>
        <el-form-item>
          <el-space>
            <el-button type="primary" @click="loginHandler()">提交</el-button>
            <el-link type="info">忘记密码？</el-link>
          </el-space>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script lang="ts">
import { reactive, ref } from "@vue/reactivity";
import { ElForm } from "element-plus/lib/components/form"
import { useRouter } from "vue-router";
import { ElMessage } from 'element-plus/lib/components/message'
import { http } from "shared/http/HttpClient"
export default {
  setup() {
    const loginForm = reactive({
      userName: "admin",
      pass: "admin",
    });
    const loading = ref<boolean>(false);
    const loginRules = {
      userName: [
        {
          required: true,
          message: "用户名不能为空",
          trigger: 'blur'
        },
      ],
      pass: [
        {
          required: true,
          message: "密码不能为空",
          trigger: 'blur'
        },
      ],
    };
    const form = ref<InstanceType<typeof ElForm>>();
    const router = useRouter();
    const loginHandler = async () => {
      loading.value = true
      await form.value?.validate((valid) => {

        if (valid) {
          http.post("/admin/account/login", { userName: loginForm.userName, password: loginForm.pass }).then(res => {
            setTimeout(() => {
              loading.value = false
              router.replace('/dash')

              ElMessage({
                message: '登录成功！', onClose: () => {
                },
                type: 'success'
              })
            }, 2000);
          })
        } else {
          loading.value = false
        }
      });
    }
    return {
      loginForm,
      loginRules,
      loginHandler,
      loading,
      form
    };
  },
};
</script>

<style lang="less" scoped>
.q-login {
  width: 100%;
  height: 100%;
  background: url("../../assets/login_bg.svg");
  padding-top: 200px;
  box-sizing: border-box;
  .q-login-box {
    width: 300px;
    padding: 2rem;
    border-radius: 5px;
    background-color: white;
    margin: 0 auto;
    box-shadow: 1px 3px 8px rgba(0, 0, 0, 0.1);
    .q-login-header {
      padding-bottom: 1rem;
      h1 {
        font-size: 18px;
        text-align: center;
        color: #303133;
      }
    }
  }
}
</style>