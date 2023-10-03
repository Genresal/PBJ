import axios from "axios";
import { API_USER_URL } from "../../../constants/ApiConstants";

export const getUser = async (id) => {
    var response = await axios.get(API_USER_URL, {
        params: {
            id: id
        }
    })

    return response.data;
}