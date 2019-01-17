<template>
  <main class="main">
    <div>
      <div class="container content-block">
          <div class="row">
              <div class="col-md-6 col-md-offset-3">
                  <h2>Login</h2>
                  <form class="login-form" @submit.prevent="submitLogin" v-if="!loginComplete">
                      <div class="form-group">
                          <label for="inputEmail">Email</label>
                          <input type="email" class="form-control" id="inputEmail" placeholder="Email" v-model="command.email">
                          <span class="error hidden"></span>
                      </div>
                      <div class="form-group">
                          <label for="inputPassword">Password</label>
                          <input type="password" class="form-control" id="inputPassword" placeholder="Password" v-model="command.password">
                          <span class="error hidden"></span>
                      </div>
                      <span class="error form-error hidden"></span>
                      <button type="submit" class="btn btn-default">Submit</button>
                  </form>
                  <div class="message" v-if="loginComplete">
                      <p>Login successful!</p>
                  </div>
              </div>
          </div>
      </div>
    </div>
  </main>
</template>

<script>

import accountApi from '@/api/auth'

export default {
  name: 'login',
  data () {
    return {
      loginComplete: false,
      command: {}
    }
  },
  methods: {
    submitLogin () {

      this.$store.dispatch('auth/login', this.command).then(() => {
        // todo
        this.loginComplete = true;
      });
    }
  }
}
</script>
