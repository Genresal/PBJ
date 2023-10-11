import axios from "axios";
import {BASE_POST_URL} from "../constants/PostApiConstants.js";

export const deletePostAsync = async (id) => {
    try {
        const response = await axios.delete(BASE_POST_URL, {
            params: {
                id: id
            }
        })
    
        return response;
    }
    catch (error) {
        console.log(error);
    }
}
