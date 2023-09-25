import axios from "axios";
import {GET_BY_USERID_URL} from "../constants/PostApiConstants.js";

export const getByUserIdAsync = async (userId, page, take) => {
    
    const response = await axios.get(GET_BY_USERID_URL, {
        params: {
            userId: userId,
            page: page,
            take: take
        }
    });

    return response;
}