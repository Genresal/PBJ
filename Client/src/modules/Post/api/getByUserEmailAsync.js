import axios from "axios";
import { API_POST_URL } from "../../../constants/ApiConstants.js";

export const getByUserEmailAsync = async (email, page, take) => {

    const response = await axios.get(API_POST_URL + "/email", {
        params: {
            email: email,
            page: page,
            take: take
        }
    });

    return response.data;
}