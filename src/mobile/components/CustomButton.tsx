import { Pressable, Text, StyleSheet, GestureResponderEvent } from "react-native"
import React, { useEffect, useState } from "react"
import colors from "../styles/colors"

interface IButtonProps {
  text: string;
  isDisabled: boolean;
  onPress: (event: GestureResponderEvent) => void;
  containerStyle?: any;
  textStyle?: any;
}

const CustomButton = (props: IButtonProps) => {
  const [disabledStyle, setDisabledStyle] = useState({});

  useEffect(() => {
    if(props.isDisabled) {
      setDisabledStyle(styles.disabledButton);
    }
    else {
      setDisabledStyle({});
    }
  }, [props.isDisabled]);

  return (
    <Pressable 
      onPress={(event: GestureResponderEvent) => {
        if(!props.isDisabled){
          props.onPress(event);
        }
      }} 
      style={[styles.container, props.containerStyle ?? {}, disabledStyle]}
      >
      <Text style={[styles.text, props.textStyle ?? {}]}>{props.text}</Text>
    </Pressable>
  )
}

const styles = StyleSheet.create({
  container: {
    backgroundColor: colors.primary,
    borderColor: colors.primary,
    width: "88%",
    borderRadius: 5,
    padding: 12,
    marginVertical: 8,
    alignItems: "center"
  },
  text: {
    color: colors.white,
    fontFamily: "sans-serif",
    fontWeight: "bold",
    fontSize: 18
  },
  disabledButton: {
    backgroundColor: colors.grey
  }
});

export { IButtonProps, CustomButton };
