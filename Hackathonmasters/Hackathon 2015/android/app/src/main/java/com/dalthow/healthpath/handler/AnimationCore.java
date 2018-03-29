package com.dalthow.healthpath.handler;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.annotation.TargetApi;
import android.os.Build;
import android.support.v7.app.AlertDialog;
import android.view.View;
import android.view.ViewAnimationUtils;

public class AnimationCore {
    public AnimationCore() {
    }

    @TargetApi(Build.VERSION_CODES.LOLLIPOP)
    public void revealShow(View rootView, boolean reveal, final AlertDialog dialog, int dialogViewID) {
        final View view = rootView.findViewById(dialogViewID);
        int w = view.getWidth();
        int h = view.getHeight();
        int tx = (int) view.getX();
        int ty = (int) view.getY();

        float maxRadius = (float) Math.sqrt(w * w + h * h) + 10;

        if (reveal) {
            Animator revealAnimator = ViewAnimationUtils.createCircularReveal(view, w / 2, h / 2, 0, maxRadius);

            view.setVisibility(View.VISIBLE);
            revealAnimator.start();

        } else {
            Animator anim = ViewAnimationUtils.createCircularReveal(view, w / 2, h / 2, maxRadius, 0).setDuration(300);
            anim.addListener(new AnimatorListenerAdapter() {
                @Override
                public void onAnimationEnd(Animator animation) {
                    super.onAnimationEnd(animation);
                    dialog.dismiss();
                    view.setVisibility(View.INVISIBLE);
                }
            });

            anim.start();
        }

    }
}