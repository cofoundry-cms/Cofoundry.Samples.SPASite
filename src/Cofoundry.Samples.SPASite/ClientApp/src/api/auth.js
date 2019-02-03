import axios from 'axios'

const BASE_URI = '/api/auth/';
const XSRF_HEADERS = ['post','put','patch','delete'];

function mapSessionInfoResponse(response) {
    const sessionInfo = response.data.data;

    for (const verb of XSRF_HEADERS) {
        axios.defaults.headers[verb]['RequestVerificationToken'] = sessionInfo.antiForgeryToken;
    }

    return sessionInfo.member;
}

export default {

    getSession() {
        return axios
            .get(BASE_URI + 'session')
            .then(mapSessionInfoResponse);
    },

    login(command) {
        return new Promise((resolve, reject) => {
            // todo yah: sort UI and refactor this
            axios
                .post(BASE_URI + 'login', command)
                .then(() => {
                    this.getSession().then(resolve);
                })
                .catch(error => {
                    const response = error.response;

                    console.log('error', response);
                    if (response.status === 400) {

                        reject(response.data.errors);
                    }
                    else {
                        // todo: handle error globally
                        alert('An unhandled error has occured');
                        reject();
                    }
                });
        });
    },

    register(command) {
        return axios
            .post(BASE_URI + 'register', command)
            .then(this.getSession)
            .catch(response => {
                console.log(response);
            });
    },

    signOut() {
        return axios
        .post(BASE_URI + 'sign-out')
        .then(this.getSession);
    }
}