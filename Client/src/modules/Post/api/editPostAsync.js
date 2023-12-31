import axios from "axios";
import {BASE_POST_URL} from "../constants/PostApiConstants.js";

export const editPostAsync = async (id, content) => {
    try {
        var response = await axios.put(BASE_POST_URL + '?id=' + id, {
            content: content
        })
    
        return response;
    }
    catch (error) {
        console.log(error);
    }
}
