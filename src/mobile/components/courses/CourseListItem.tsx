import { View, Text, StyleSheet, ScrollView } from "react-native";
import React from "react";
import { TagItem } from "./TagItem";
import { ICourseInfo } from "../../api/courses/getAllCoursesInfos";
import { CustomButton, IButtonProps } from "../CustomButton";

export interface ICourseListItemProps {
  course: ICourseInfo;
  button: IButtonProps
}

export const CourseListItem = (props: ICourseListItemProps) => {
  return (
    <ScrollView style={styles.root}>
      <View style={styles.main}>
        <Text style={styles.title}>{props.course.name}</Text>
        <View style={styles.tags}>
          <TagItem
            text={`XP ${props.course.xp}`}
          />
          <TagItem
            text={`Treshold: ${props.course.passPercentTreshold}`}
          />
        </View>
      </View>
      <View style={styles.secondary}>
        <Text>{props.course.description}</Text>
        <View style={styles.button}>
          <CustomButton
            {...props.button}
            textStyle={styles.buttonText}
            containerStyle={styles.buttonContainer}
          />
        </View>
      </View>
    </ScrollView >
  )
}

const styles = StyleSheet.create({
  root: {
    borderRadius: 15,
    backgroundColor: "pink",
    marginTop: 12,
    marginLeft: "6%",
    marginRight: "6%",
    paddingTop: "2%",
    paddingLeft: "4%",
    paddingBottom: "2%",
    paddingRight: "4%"
  },
  main: {
    flex: 1,
    flexDirection: "row",
    justifyContent: "flex-start",
    alignItems: "baseline"
  },
  tags: {
    flex: 1,
    flexDirection: "row",
    justifyContent: "flex-end",
  },
  secondary: {
    flex: 1,
    flexDirection: "row",
    alignItems: "baseline",
    marginTop: 10
  },
  button: {
    flex: 1,
    flexDirection: "row",
    justifyContent: "flex-end",
    width: 80
  },
  buttonText: {
    fontWeight: "normal",
    fontSize: 14
  },
  buttonContainer: {
    width: 80,
    padding: 6,
    marginVertical: 0
  },
  title: {
    fontWeight: "bold",
    fontSize: 18
  },
  description: {
    width: "70%"
  }
});
