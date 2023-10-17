import axios from "axios";
import { API_POST_URL } from "../../../constants/ApiConstants";

export const getByIdAsync = async (id) => {
    try {
        const response = await axios.get(API_POST_URL, {
            params : {
                id: id
            }
        })

        return response;
    }
    catch (error) {
        console.log(error);
    }
}