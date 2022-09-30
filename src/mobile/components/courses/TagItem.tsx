import { View, Text, StyleSheet } from "react-native";
import React from "react";
import colors from "../../styles/colors";

export interface ITagProps {
  text: string;
}

export const TagItem = (props: ITagProps) => {
  return (
    <View style={styles.root}>
      <Text style={styles.text}>{props.text}</Text>
    </View>
  )
}

const styles = StyleSheet.create({
  root: {
    borderRadius: 20,
    backgroundColor: colors.primaryLight,
    paddingTop: 2,
    paddingBottom: 2,
    paddingLeft: 8,
    paddingRight: 8,
    marginLeft: 5
  },
  text: {
    fontSize: 12,
    color: colors.white
  }
});
