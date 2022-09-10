import { Pressable, Text, StyleSheet, GestureResponderEvent } from "react-native"
import React, { useEffect, useState } from "react"
import colors from "../styles/colors"

interface IButtonProps {
  text: string;
  isDisabled: boolean;
  onPress: (event: GestureResponderEvent) => Promise<void>;
  containerStyle?: any;
  textStyle?: any;
}

const CustomButton = (props: IButtonProps) => {
  const pressableStandardStyles = [styles.container, props.containerStyle ?? {}];
  const [pressableStyles, setPressableStyles] = useState(pressableStandardStyles);

  useEffect(() => {
    if(props.isDisabled) {
      setPressableStyles([...pressableStandardStyles, styles.disabledButton]);
    }
    else {
      setPressableStyles(pressableStandardStyles);
    }
  }, [props.isDisabled]);

  return (
    <Pressable 
      onPress={async (event: GestureResponderEvent) => {
        if(!props.isDisabled){
          await props.onPress(event);
        }
      }} 
      style={pressableStyles}
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
