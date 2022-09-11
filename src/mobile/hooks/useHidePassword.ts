import { useState } from "react";

export const useHidePassword = () => {
  const showIcon = "eye-outline";
  const hideIcon = "eye-off-outline";
  const [hidePassword, setHidePassword] = useState(true);
  const [endIconName, setEndIconName] = useState(hideIcon);
  const [secureTextEntry, setSecureTextEntry] = useState(true);

  const hide = () => {
    setHidePassword(!hidePassword);
    if(hidePassword) {
      setEndIconName(hideIcon);
      setSecureTextEntry(true);
    }
    else {
      setEndIconName(showIcon);
      setSecureTextEntry(false);
    }
  }

  return (
    {
      hide,
      hidePassword,
      endIconName,
      secureTextEntry
    }
  )
}
