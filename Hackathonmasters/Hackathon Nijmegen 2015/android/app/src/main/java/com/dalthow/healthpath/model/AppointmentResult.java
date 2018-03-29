package com.dalthow.healthpath.model;

import java.util.ArrayList;
import java.util.List;

public class AppointmentResult
{
    private String prescription, diagnose;
    private Appointment nextAppointment;
    private List<AppointmentResultQuestions> questions = new ArrayList<AppointmentResultQuestions>();

    public String getPrescription() {
        return prescription;
    }

    public String getDiagnose() {
        return diagnose;
    }

    public void setDiagnose(String diagnose) {
        this.diagnose = diagnose;
    }

    public void setPrescription(String prescription) {
        this.prescription = prescription;
    }

    public Appointment getNextAppointment() {
        return nextAppointment;
    }

    public void setNextAppointment(Appointment nextAppointment) {
        this.nextAppointment = nextAppointment;
    }

    public List<AppointmentResultQuestions> getQuestions() {
        return questions;
    }

    public void setQuestions(List<AppointmentResultQuestions> questions) {
        this.questions = questions;
    }
}
