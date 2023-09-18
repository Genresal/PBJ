import axios from "axios";
import {BASE_USER_URL} from "../constants/UserApiConstants.js";


export const getUser = async (id) => {
    var response = await axios.get(BASE_USER_URL, {
        params: {
            id: id
        }
    })

    return response.data;
}