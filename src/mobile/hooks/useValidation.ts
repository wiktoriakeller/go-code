import { useEffect } from "react";
import { IValidation } from "../validation/validators";

const useValidation = <T>(props: IValidation<T>) => {
  const updatingValues =
    props.changingValues != null ? props.changingValues : [props.value];

  useEffect(() => {
    for (const validationFunc of props.validators) {
      const result = validationFunc(props.value);
      if (!result[0]) {
        props.error.setMessage(result[1]);
        return;
      }
    }

    props.error.setMessage("");
  }, [...updatingValues]);
};

export { useValidation };
