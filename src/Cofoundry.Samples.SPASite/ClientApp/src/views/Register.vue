<template>
  <main class="main">
    <div>

      <div class="container content-block">
          <div class="row">
              <div class="col-md-6 col-md-offset-3">
                  <h2>Register</h2>

                  <form class="register-form"
                        @submit.prevent="submitRegistration" 
                        v-if="!registrationComplete"
                        autocomplete="off">

                      <div class="form-group">
                          <label for="inputName">First Name</label>
                          <input type="text" name="firstname" class="form-control" id="inputName" placeholder="First Name"
                                 v-model="command.firstName">
                          <span class="error hidden"></span>
                      </div>
                      <div class="form-group">
                          <label for="inputSurname">Last Name</label>
                          <input type="text" name="lastname" class="form-control" id="inputSurname" placeholder="Last Name"
                                 v-model="command.lastName">
                          <span class="error hidden"></span>
                      </div>
                      <div class="form-group">
                          <label for="inputEmail">Email address</label>
                          <input type="email" name="email" class="form-control" id="inputEmail" placeholder="Email"
                                 v-model="command.email">
                          <span class="error hidden"></span>
                      </div>
                      <div class="form-group">
                          <label for="inputPassword">Password</label>
                          <input type="password" name="password" class="form-control" id="inputPassword" placeholder="Password"
                                 v-model="command.password">
                          <span class="error hidden"></span>
                      </div>
                      <span class="error form-error hidden"></span>
                      <button type="submit" class="btn btn-default">Submit</button>
                  </form>

                  <div class="message"
                      v-if="registrationComplete">
                      <p>Thank you for registering with SPA Cats! You now have access to extra features, such as favouriting the cats you love the most.</p>
                      <p>Enjoy!</p>
                  </div>
              </div>
          </div>
      </div>

    </div>
  </main>
</template>

<script>

export default {
  name: 'registration',
  data () {
    return {
      registrationComplete: false,
      command: {}
    }
  },
  methods: {
    submitRegistration () {

      this.$store.dispatch('auth/register', this.command).then(() => {
        // todo - validate/loading
        this.registrationComplete = true;
      });
    }
  }
}
</script>
