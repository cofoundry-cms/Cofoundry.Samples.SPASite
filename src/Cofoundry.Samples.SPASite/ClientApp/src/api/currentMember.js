import axios from 'axios'

const BASE_URI = '/api/members/current/';

export default {

    getLikedCats() {
        return axios
            .get(BASE_URI + 'cats/liked')
            .then(response => {
                return response.data.data;
            });
    }
}