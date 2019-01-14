import axios from 'axios'

const BASE_URI = '/api/cats/';

export default {
    searchCats() {
        return axios.get(BASE_URI)
            .then(response => {
                return response.data.data;
            });
    },

    getCatById(id) {
        return axios.get(BASE_URI + id)
        .then(response => {
            return response.data.data;
        });
    }
}