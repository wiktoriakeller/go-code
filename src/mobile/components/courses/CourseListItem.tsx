import { View, Text, StyleSheet, ScrollView } from "react-native";
import React from "react";
import { TagItem } from "./TagItem";
import { ICourseInfo } from "../../api/courses/getAllCoursesInfosRequest";
import { CustomButton, IButtonProps } from "../CustomButton";
import { IUserCourse } from "../../api/courses/getUserCoursesRequest";
import colors from "../../styles/colors";

export interface ICourseListItemProps {
  course: ICourseInfo | IUserCourse;
  button: IButtonProps
}

export const CourseListItem = (props: ICourseListItemProps) => {
  return (
    <ScrollView style={styles.root}>
      <View style={styles.main}>
        <Text style={styles.title}>{props.course.name}</Text>
        <View style={styles.tags}>
          <TagItem
            text={`Treshold: ${props.course.passPercentTreshold}`}
          />
          {
            (props.course as IUserCourse)?.userPassed === true ?
            <TagItem
              text={"Passed"}
            /> : <View/>
          }
        </View>
      </View>
      <View style={styles.secondary}>
        <View style={styles.description}>
          <Text style={ {fontSize: 16} }>{props.course.description}</Text>
        </View>
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
    borderColor: colors.lightGrey,
    backgroundColor: colors.light,
    marginTop: 6,
    marginBottom: 6,
    marginLeft: "6%",
    marginRight: "6%",
    paddingTop: "2%",
    paddingLeft: "4%",
    paddingBottom: "2%",
    paddingRight: "4%",
    shadowColor: "#000",
    shadowOffset: {
      width: 2,
      height: 2
    },
    shadowOpacity: 0.4,
    shadowRadius: 4,
    elevation: 3
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
    alignItems: "center",
    marginTop: 10
  },
  button: {
    width: "25%"
  },
  buttonText: {
    fontWeight: "normal",
    fontSize: 14
  },
  buttonContainer: {
    width: "100%",
    padding: 6,
    marginVertical: 0,
  },
  title: {
    fontWeight: "bold",
    fontSize: 18
  },
  description: {
    width: "75%"
  }
});
