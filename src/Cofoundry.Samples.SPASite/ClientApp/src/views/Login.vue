<template>
    <main class="main">
        <div>
            <div class="container content-block">
                <div class="row">
                    <div class="col-md-6 col-md-offset-3">
                        <h2>Login</h2>
                        <form
                            class="login-form"
                            @submit.prevent="submitLogin"
                            v-if="!loginComplete"
                        >
                            <div class="form-group">
                                <label for="inputEmail">Email</label>
                                <input
                                    type="email"
                                    class="form-control"
                                    id="inputEmail"
                                    placeholder="Email"
                                    required
                                    v-model="command.email"
                                >
                                <span class="error hidden"></span>
                            </div>
                            <div class="form-group">
                                <label for="inputPassword">Password</label>
                                <input
                                    type="password"
                                    class="form-control"
                                    id="inputPassword"
                                    placeholder="Password"
                                    required
                                    v-model="command.password"
                                >
                                <span class="error hidden"></span>
                            </div>
                            <validation-summary :errors="errors"/>
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
import accountApi from "@/api/auth";
import ValidationSummary from "@/components/ValidationSummary";

export default {
    name: "login",
    components: {
        ValidationSummary
    },
    data() {
        return {
            loginComplete: false,
            command: {},
            errors: []
        };
    },
    methods: {
        submitLogin() {
            const me = this;

            this.$store
                .dispatch("auth/login", this.command)
                .then(loginComplete)
                .catch(loginFailed);

            function loginComplete() {
                me.loginComplete = true;
                me.errors = [];
            }

            function loginFailed(errors) {
                me.errors = errors;
            }
        }
    }
};
</script>
